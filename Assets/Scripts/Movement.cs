using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Movement : MonoBehaviour
{
    /// <summary>
    /// Player movement fields
    /// </summary>
    public float speed;
    public float jumpPower;
    private float moveHorizontal;

    private Rigidbody2D rb2d;

    // Animation related fields
    private Animator animations;
    private bool crouch;
    private bool canDoubleJump;
    private bool grounded;

    private void Start()
    {
        // Gets the RigidBody and Animations attached to the player
        rb2d = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RunAnimations();
        ApplyPlayerMovement();
    }


    /// <summary>
    /// Changes player velocity based on the keyboard/controller input
    /// </summary>
    void ApplyPlayerMovement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        setSpriteDirection();
        Jump();
    }

    //Sets direction of player sprite
    void setSpriteDirection()
    {

        if (moveHorizontal < -.1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveHorizontal > .1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || canDoubleJump)
            {
                crouch = true;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            crouch = false;
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                grounded = false;
                canDoubleJump = true;
            } else if (canDoubleJump)
            {
                canDoubleJump = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpPower);
            }
        }
    }

    // Checks whether player is touching ground or not
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    // Ensures that Animation booleans are associated on each update
    void RunAnimations()
    {
        animations.SetFloat("Movement", Mathf.Abs(moveHorizontal));
        animations.SetBool("IsCrouching", crouch);
        animations.SetBool("Grounded", grounded);
    }
}
