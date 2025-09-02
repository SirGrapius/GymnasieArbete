using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public bool isMoving;
    [SerializeField] float speed = 5;
    [SerializeField] bool sprinting;

    Vector2 playerInput;

    [Header("Audio")]
    // audio controller script here
    [SerializeField] AudioSource walkSource;
    [Header("Anim Settings")]
    [SerializeField] Animator animator;

    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerInput.x != 0)
        {
            isMoving = true;
            if (playerInput.x < 0)
            {
                rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (playerInput.x > 0)
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (playerInput.y != 0)
        {
            if (playerInput.y < 0)
            {
                //change animation to move downwards
            }
            if (playerInput.y > 0)
            {
                //change animation to move upwards
            }
        }

        while (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            sprinting = true;
        }

        if (sprinting)
        {
            speed = speed * 1.5f;
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = playerInput.x * speed;
        rb.linearVelocityY = playerInput.y * speed;
    }
}
