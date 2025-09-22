using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public bool isMoving;
    [SerializeField] float baseSpeed = 5;
    [SerializeField] float currentSpeed;
    [SerializeField] bool sprinting;

    [Header("Combat Settings")]
    [SerializeField] int hitPoints;
    [SerializeField] float combatSpeed;
    [SerializeField] bool hasiFrames;
    [SerializeField] public float baseDamage;
    [SerializeField] public float damageModifier;

    Vector2 playerInput;

    [Header("Audio")]
    // audio controller script here
    [SerializeField] AudioSource walkSource;
    [Header("Anim Settings")]
    [SerializeField] Animator animator;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Rigidbody2D mainRigidbody;
    [SerializeField] Rigidbody2D combatRigidbody;

    [SerializeField] Transform spriteTransform;

    private void Awake()
    {
        mainRigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb = mainRigidbody;
        animator = GetComponentInChildren<Animator>();
        currentSpeed = baseSpeed;
    }


    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerInput.x != 0)
        {
            isMoving = true;
            if (playerInput.x < 0)
            {
                spriteTransform.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (playerInput.x > 0)
            {
                spriteTransform.transform.rotation = Quaternion.Euler(0, 0, 0);
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

        if (rb.linearVelocityX != 0 &&  rb.linearVelocityY != 0)
        {
            rb.linearVelocityX = rb.linearVelocityX * Time.deltaTime / Mathf.Sqrt(2);
            rb.linearVelocityY = rb.linearVelocityY * Time.deltaTime / Mathf.Sqrt(2);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !sprinting || Input.GetKeyDown(KeyCode.RightShift) && !sprinting)
        {
            sprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            sprinting = false;
        }

        if (sprinting)
        {
            currentSpeed = baseSpeed * 1.5f;
        }
        else
        {
            currentSpeed = baseSpeed;
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = playerInput.x * currentSpeed;
        rb.linearVelocityY = playerInput.y * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && !hasiFrames)
        {
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            StartCoroutine(GetHit(bullet.damage, bullet.immunityDuration));
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && hasiFrames)
        {
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator GetHit(int damageTaken, float iFrameDuration)
    {
        hitPoints -= damageTaken;
        hasiFrames = true;
        yield return new WaitForSeconds(iFrameDuration);
        hasiFrames = false;
        yield return null;
    }

    public void EnterCombat()
    {
        rb = combatRigidbody;
    }

    public void LeaveCombat()
    {
        rb = mainRigidbody;
    }
}
