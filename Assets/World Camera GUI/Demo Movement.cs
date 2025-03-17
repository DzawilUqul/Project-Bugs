using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float speedX;
    private float speedY;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate ()
    {
        rb.velocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
    }
}

