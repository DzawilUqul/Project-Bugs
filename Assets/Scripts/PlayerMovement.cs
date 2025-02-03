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

    [Header("Gravity Settings")]
    public float fallMultiplier = 2.5f; // Multiplier for faster falling
    public float lowJumpMultiplier = 2f; // For shorter jumps when jump button is released
    public float holdJumpMultiplier = 1.2f; // Extend jump height when button is held

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        ApplyCustomGravity();
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

        // Extend jump height while holding the jump button
        if (Input.GetButton("Jump") && rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (holdJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void ApplyCustomGravity()
    {
        if (rb.velocity.y < 0) // Player is falling
        {
            // Apply fall multiplier
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // Jump button released early
        {
            // Apply low jump multiplier
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualisasi groundCheck di Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
