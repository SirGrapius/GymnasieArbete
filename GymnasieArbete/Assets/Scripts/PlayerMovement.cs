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
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = playerInput.x * speed;
        rb.linearVelocityY = playerInput.y * speed;
    }
}
