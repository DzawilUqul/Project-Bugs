using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float meleeAttackRange = 1.5f;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private LayerMask enemyLayer;

    private bool isAttacking;

    public void PerformMeleeAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        animator.SetTrigger("Attack");

        Vector2 attackPosition = (Vector2)transform.position + Vector2.right * transform.localScale.x * meleeAttackRange;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, meleeAttackRange, enemyLayer);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            HandleDamage(enemy);
        }

        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void HandleDamage(Collider2D enemy)
    {
        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(meleeDamage);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPosition = (Vector2)transform.position + Vector2.right * transform.localScale.x * meleeAttackRange;
        Gizmos.DrawWireSphere(attackPosition, meleeAttackRange);
    }
}