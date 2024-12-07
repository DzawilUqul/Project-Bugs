using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck; // Posisi untuk memeriksa tanah
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer untuk tanah

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); // A/D atau Arrow Keys
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Optional: Flip player sprite based on direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1); // Menghadap kanan
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Menghadap kiri
    }

    void Jump()
    {
        // Periksa apakah player di tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualisasi groundCheck di Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
