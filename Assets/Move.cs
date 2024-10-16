using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    public float moveSpeed = 5f;       // Speed for horizontal movement
    public float jumpForce = 7f;       // Jumping force
    private Vector3 moveDirection;     // Movement vector
    private bool isGrounded = true;    // Ground detection
    private Rigidbody rb;              // Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Maintain current Y velocity for jump/fall, and apply movement to X axis
        moveDirection = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);

        // Jump logic
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply jump force
            isGrounded = false; // Set to false while in the air
        }

        // Move the player by assigning velocity
        rb.velocity = moveDirection * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detect ground collision
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Allow jumping again when grounded
        }
    }
}
