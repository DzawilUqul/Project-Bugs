using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}

public enum MovementType
{
    Ground,
    Flying
}

public enum AttackType
{
    Melee,
    Ranged
}

[System.Serializable]
public class EnemyParameters
{
    public MovementType movementType;
    public AttackType attackType;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public Transform[] patrolPoints;
    public Vector3 offset;
}

public class EnemyAI : MonoBehaviour
{
    public EnemyParameters parameters;
    public EnemyState currentState;

    private Transform player;
    private int currentPatrolIndex;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentState = EnemyState.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Patrol:
                HandlePatrol();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
        }

        TransitionStates();
    }

    private void HandleIdle()
    {
    }

    private void HandlePatrol()
    {
        if (parameters.patrolPoints == null || parameters.patrolPoints.Length == 0) return;

        Transform targetPoint = parameters.patrolPoints[currentPatrolIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, parameters.patrolSpeed * Time.deltaTime);

        Debug.Log(Vector2.Distance(transform.position, targetPoint.position));
        if (Vector2.Distance(transform.position, targetPoint.position) <= 0.1f)
        {
            Debug.Log("Reached patrol point");
            currentPatrolIndex = (currentPatrolIndex + 1) % parameters.patrolPoints.Length;
        }
    }

    private void HandleChase()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Stop moving if within a certain threshold (e.g., slightly less than attack range)
        if (distanceToPlayer < parameters.attackRange * 0.9f)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, parameters.chaseSpeed * Time.deltaTime);
    }


    private void HandleAttack()
    {
        if (player == null || Time.time - lastAttackTime < parameters.attackCooldown) return;

        if (parameters.attackType == AttackType.Melee)
        {
            Debug.Log("Enemy performs melee attack");
        }
        else if (parameters.attackType == AttackType.Ranged && parameters.projectilePrefab != null)
        {
            GameObject projectile = Instantiate(parameters.projectilePrefab, transform.position + parameters.offset, Quaternion.identity);
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                projectileRb.velocity = direction * parameters.projectileSpeed;
            }
        }

        lastAttackTime = Time.time;
    }

    private void TransitionStates()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < parameters.attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer < parameters.detectionRange)
        {
            currentState = EnemyState.Chase;
        }
        else if (parameters.patrolPoints != null && parameters.patrolPoints.Length > 0)
        {
            currentState = EnemyState.Patrol;
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }
}
