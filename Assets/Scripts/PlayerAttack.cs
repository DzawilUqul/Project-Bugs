using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2D : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAttackMelee meleeAttack;
    [SerializeField] private PlayerAttackProjectile projectileAttack;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            meleeAttack.PerformMeleeAttack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            projectileAttack.ShootProjectile();
        }
    }
}