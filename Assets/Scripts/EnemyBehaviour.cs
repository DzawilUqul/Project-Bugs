using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool isInRange;
    public Transform hotZone;
    public Transform triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance;
    private bool isAttacking;
    private bool isCooling;
    private float intTimer;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (!isAttacking)
        {
            Move();
        }

        if (!IsInsideOfLimits() && !isInRange && !anim.GetAnimatorTransitionInfo(0).IsName("Enemy_Attack"))
        {
            SelectTarget();
        }

        if (isInRange)
        {
            EnemyLogic();
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && isCooling == false)
        {
            Attack();
        }

        if (isCooling)
        {
            Cooldown();
            // anim.SetBool("Attack", false);
        }
    }

    private void Move()
    {
        // anim.SetBool("canWalk", true);

        if (!anim.GetAnimatorTransitionInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // if player is left then flip left
            if (transform.position.x > target.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
        timer = intTimer;
        isAttacking = true;

        // anim.SetBool("canWalk", false);
        // anim.SetBool("Attack", true);
    }

    private void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && isCooling && isAttacking)
        {
            isCooling = false;
            timer = intTimer;
        }
    }

    private void StopAttack()
    {
        isCooling = false;
        isAttacking = false;
        // anim.SetBool("Attack", false);
    }

    public void TriggerisCooling()
    {
        isCooling = true;
    }

    private bool IsInsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }
}
