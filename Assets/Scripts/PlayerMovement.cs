using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private float jumpCount;
    private float maxJumps = 2;

    [Header("Ground Check")]
    public Transform groundCheck; // Posisi untuk memeriksa tanah
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer untuk tanah
    private bool isGrounded;

    [Header("Gravity Settings")]
    public float fallMultiplier = 2.5f; // Multiplier for faster falling
    public float lowJumpMultiplier = 2f; // For shorter jumps when jump button is released
    public float holdJumpMultiplier = 1.2f; // Extend jump height when button is held

    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;
    private float dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            Move();
            Jump();
            ApplyCustomGravity();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
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
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
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

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        dashDirection = transform.localScale.x; // Dash to current direction
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero; // Stop after dash

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
