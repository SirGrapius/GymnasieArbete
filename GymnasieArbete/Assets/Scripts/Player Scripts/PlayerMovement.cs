using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] GameManager gameManager;
    [SerializeField] CombatController combatManager;

    [Header("Movement Settings")]
    [SerializeField] public bool movementEnabled = true; //bool that allows the player to move
    [SerializeField] public bool isMoving;
    [SerializeField] public float baseSpeed = 5;
    [SerializeField] public float currentSpeed;
    [SerializeField] bool sprinting;
    [SerializeField] BoxCollider2D baseCollider; //the collider
    Vector2 playerInput;

    [Header("Dialogue Settings")]
    [SerializeField] TextBoxHandler dialogueScript;
    [SerializeField] public NPCScript currentNPC;
    [SerializeField] public bool inDialogue;

    [Header("Combat Settings")]
    [SerializeField] Vector3 originalPosition; //the player's position right before teleporting
    [SerializeField] public GameObject arena; //the position of the arena
    [SerializeField] public int maxHitPoints;
    [SerializeField] public int hitPoints;
    [SerializeField] float combatSpeed; //the player's speed when in combat
    [SerializeField] bool hasiFrames;
    [SerializeField] public float baseDamage; //the player's base damage with their weapon
    [SerializeField] public float damageModifier; //a weapons base damage modifier, basically just allows certain weapons to get better multipliers
    [SerializeField] public int experience;

    [Header("Audio")]
    // audio controller script here
    [SerializeField] AudioSource walkSource;
    [Header("Anim Settings")]
    [SerializeField] Animator animator;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject spriteObject;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites; //0 = overworld, 1 = combat

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentSpeed = baseSpeed;
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        combatManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<CombatController>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (movementEnabled)
        {
            currentSpeed = baseSpeed;
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            currentSpeed = 0;
        }

        if (playerInput.x != 0) //moves the player horizontally
        {
            isMoving = true;
            if (playerInput.x < 0)
            {
                spriteObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (playerInput.x > 0)
            {
                spriteObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !sprinting && !combatManager.combatOnGoing || Input.GetKeyDown(KeyCode.RightShift) && !sprinting && !combatManager.combatOnGoing)
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
        spriteRenderer.color = Color.black;
        hitPoints -= damageTaken;
        hasiFrames = true;
        yield return new WaitForSeconds(iFrameDuration);
        spriteRenderer.color = Color.white;
        hasiFrames = false;
        yield return null;
    }

    public IEnumerator EnterCombat()
    {
        originalPosition = this.transform.position; //saves position
        StartCoroutine(gameManager.FadeOutCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        spriteRenderer.sprite = sprites[1]; //changes the player's sprite to the combat sprite
        this.baseCollider.size = new Vector2(0.5f, 0.5f); //changes the size of the player's collider
        StartCoroutine(gameManager.FadeInCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        this.transform.position = arena.transform.position; //teleports player to arena
        cameraFollow.inCombat = true; //stops the camera from following the player
        combatManager.StartCombat(); //activates the combat controller
        yield return null;
    }

    public IEnumerator LeaveCombat()
    {
        StartCoroutine(gameManager.FadeOutCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        spriteRenderer.sprite = sprites[0];
        this.baseCollider.size = new Vector2(1f, 1f);
        StartCoroutine(gameManager.FadeInCoroutine());
        yield return new WaitForSeconds(gameManager.fadeDuration);
        this.transform.position = originalPosition; //returns player to original position
        cameraFollow.inCombat = false;
        yield return null;
    }
}
