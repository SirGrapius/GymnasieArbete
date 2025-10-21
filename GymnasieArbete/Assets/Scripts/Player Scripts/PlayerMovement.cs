using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] GameManager gameManager;

    [Header("Movement Settings")]
    [SerializeField] public bool movementEnabled = true; //bool that allows the player to move
    [SerializeField] public bool isMoving;
    [SerializeField] public float baseSpeed = 5;
    [SerializeField] public float currentSpeed;
    [SerializeField] bool sprinting;
    [SerializeField] BoxCollider2D baseCollider; //the collider of the player's overworld object
    [SerializeField] BoxCollider2D combatCollider; //the collider of the player's combat object
    Vector2 playerInput;

    [Header("Dialogue Settings")]
    [SerializeField] TextBoxHandler dialogueScript;
    [SerializeField] public NPCScript currentNPC;
    [SerializeField] public bool inDialogue;

    [Header("Combat Settings")]
    [SerializeField] Vector3 originalPosition; //the player's position right before teleporting
    [SerializeField] public GameObject arena; //the position of the arena
    [SerializeField] public int hitPoints;
    [SerializeField] float combatSpeed; //the player's speed when in combat
    [SerializeField] bool hasiFrames;
    [SerializeField] public float baseDamage; //the player's base damage with their weapon
    [SerializeField] public float damageModifier; //a modifier applied to the player's damage based on how well they did on the attack minigame
    [SerializeField] public int experience;

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
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }


    void Update()
    {
        if (movementEnabled)
        {
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        if (playerInput.x != 0) //moves the player horizontally
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

        if (playerInput.y != 0) //moves the player vertically
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

        if (Input.GetKeyDown(KeyCode.Z) && currentNPC != null && !inDialogue) //interact with NPC
        {
            dialogueScript.StartNewDialogue(currentNPC);
            inDialogue = true;
            movementEnabled = false;
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
        if (collision.CompareTag("Bullet") && !hasiFrames)
        {
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            StartCoroutine(GetHit(bullet.damage, bullet.immunityDuration));
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Bullet") && hasiFrames)
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

    public IEnumerator EnterCombat()
    {
        originalPosition = this.transform.position; //saves position
        StartCoroutine(gameManager.FadeOutCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        StartCoroutine(gameManager.FadeInCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        this.transform.position = arena.transform.position; //teleports player to arena
        cameraFollow.inCombat = true;
        baseCollider.enabled = false;
        combatCollider.enabled = true;
        yield return null;
    }

    public IEnumerator LeaveCombat()
    {
        StartCoroutine(gameManager.FadeOutCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        StartCoroutine(gameManager.FadeInCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        this.transform.position = originalPosition; //returns player to original position
        cameraFollow.inCombat = false;
        baseCollider.enabled = true;
        combatCollider.enabled = false;
        yield return null;
    }
}
