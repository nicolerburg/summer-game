using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Burg : MonoBehaviour
{

    /* Script for player movement */

    // Variables
    [Header("Movement Values")]
    public float walkSpeed = 500;
    public int maxJumps = 2;
    private int jumps;
    public float jumpForce = 400;
    //public bool canMoveInAir = true;
    private float moveInput;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    private float boxCastDepth = 0.01f;
    //private bool isGrounded = false;


    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        jumps = 0;
    }

    void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * walkSpeed, rigidBody.velocity.y);
        //Debug.Log("Fixed " + jumps);
    }

    // Update is called once per frame
    void Update() {
        ComputeIsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && jumps < maxJumps) {
            rigidBody.velocity = Vector2.up * jumpForce;
            ++jumps;
            //Debug.Log("Jumps= " + jumps);
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            //Debug.Log("Can't jump, Jumps= " + jumps);
        }
        UpdateDirection();
    }

    private void ComputeIsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, boxCastDepth, groundLayer);
        Color color;
        if (hit.collider == null) {
            //isGrounded = false;
            color = Color.red;
        }
        else {
            //isGrounded = true;
            if (rigidBody.velocity.y <= 0) {
                //Debug.Log(" Grounded Jumps = " + jumps);
                jumps = 0;
                //Debug.Log("Jumps reset " + jumps);
            }
            color = Color.green;
        }
        Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x, 0), Vector2.down * (collider.bounds.extents.y + boxCastDepth), color);
        Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, 0), Vector2.down * (collider.bounds.extents.y + boxCastDepth), color);
        Debug.DrawRay(collider.bounds.center + new Vector3(-collider.bounds.extents.x, -collider.bounds.extents.y - boxCastDepth), Vector2.right * (collider.bounds.extents.x * 2), color);
    }

    void UpdateDirection() {
        if (moveInput < 0) {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0) {
            spriteRenderer.flipX = false;
        }
    }

    public void changeSpeed(float percentage) {
        walkSpeed = walkSpeed * percentage;
    }
}
