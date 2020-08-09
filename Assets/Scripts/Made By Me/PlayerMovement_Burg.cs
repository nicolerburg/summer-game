using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [Range(0.0f, 1.0f)]
    public float airDrag = 0.1f;
    public float ladderCheckHeight;

    [HideInInspector]
    public bool frozen = false;
    private float moveInput;
    private float inputVertical;
    public Animator animator;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    private float boxCastDepth = 0.01f;
    private bool isGrounded = false;
    private bool isClimbing = false;


    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        jumps = 0;
    }

    void FixedUpdate() {

        moveInput = Input.GetAxis("Horizontal");
        if (!frozen) {
            rigidBody.velocity = new Vector2(moveInput * walkSpeed, rigidBody.velocity.y);
        }
        else {
            rigidBody.velocity = new Vector3(0.0f, rigidBody.velocity.y, 0.0f);
        }

        animator.SetFloat("speed", Math.Abs(rigidBody.velocity.x));

        if (isGrounded) {
            jumps = 0;
        } else {
            //UnityEngine.Debug.Log("Airborne");
            Vector2 vel = rigidBody.velocity;
            vel.x *= (1.0f - airDrag);
            rigidBody.velocity = vel; //applies horizontal drag while airborne
        }

        RaycastHit2D ladderHit = Physics2D.Raycast(transform.position, Vector2.up, ladderCheckHeight, ladderLayer);

        if(ladderHit.collider != null) {
            if (Input.GetKeyDown(KeyCode.W)) {
                isClimbing = true;
            }
        } else {
            isClimbing = false;
        }

        if(isClimbing) {
            inputVertical = Input.GetAxis("Vertical");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, inputVertical * walkSpeed);
            rigidBody.gravityScale = 0;
        } else {
            rigidBody.gravityScale = 1.2f;
        }

    }

    // Update is called once per frame
    void Update() {
        ComputeIsGrounded();
        if (isGrounded) {
            animator.SetBool("isJumping", false);
        }
        if (!frozen) {
            if (Input.GetKeyDown(KeyCode.Space) && jumps < maxJumps) {
                rigidBody.velocity = Vector2.up * jumpForce;
                animator.SetBool("isJumping", true);
                ++jumps;
                //Debug.Log("Jumps= " + jumps);
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                //Debug.Log("Can't jump, Jumps= " + jumps);
            }
        }
        if (!frozen) {
            UpdateDirection();
        }
    }

    private void ComputeIsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, boxCastDepth, groundLayer);
        Color color;
        if (hit.collider && rigidBody.velocity.y <= 0) {
            isGrounded = true;
            color = Color.green;
        } else {
            isGrounded = false;
            color = Color.red;
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
