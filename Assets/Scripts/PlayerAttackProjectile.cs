using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackProjectile : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    private bool isAttacking;

    public void ShootProjectile()
    {
        if (isAttacking) return;

        isAttacking = true;
        animator.SetTrigger("Shoot");

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(transform.localScale.x * projectileSpeed, 0f);
        }

        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}