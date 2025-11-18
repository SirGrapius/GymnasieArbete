using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] public BoxCollider2D myCollider;
    [SerializeField] Transform playerTransform;
    [SerializeField] public SpriteRenderer spriteRenderer;

    [SerializeField] float speed;

    [Header("Hit Effects")]
    [SerializeField] public int damage;
    [SerializeField] public float immunityDuration;

    [Header("Basic Bullet Types")]
    [SerializeField] public bool autoAimBullet;
    [SerializeField] public bool verticalBullet;
    [SerializeField] public bool horizontalBullet;
    [SerializeField] public bool aoeBullet; //the big warning sign then half the arena gets nuked or whatever
    [SerializeField] public bool explodingBullet;
    [SerializeField] public bool rotatingBullet;

    [Header("AoE Bullet Variables")]
    [SerializeField] public int amountOfAoE;
    [SerializeField] public float startUpTime;
    [SerializeField] public float timeBetween; //time between aoe striking and it disappearing
    [SerializeField] public Sprite[] sprites; //tempAttackSprite is 2, vertical warning is 0, horizontal warning is 1, verticletempattack is 3


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    void Start()
    {
        if (autoAimBullet)
        {
            IsAutoAimBullet();
        }
        if (aoeBullet)
        {
            myCollider.enabled = false;
            StartCoroutine(IsAoEBullet());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RotateTowards(Vector2 target)
    {
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
    }

    void IsAutoAimBullet()
    {
        RotateTowards(playerTransform.position);
        myRigidBody.linearVelocity = transform.right * speed;
    }

    void IsHorizontalBullet()
    {

    }

    void IsVerticalBullet()
    {

    }

    IEnumerator IsAoEBullet()
    {
        yield return new WaitForSeconds(startUpTime);
        myCollider.enabled = true;
        if (spriteRenderer.sprite == sprites[1])
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (spriteRenderer.sprite == sprites[0])
        {
            spriteRenderer.sprite = sprites[3];
        }
        yield return new WaitForSeconds(timeBetween);
        Destroy(gameObject);
        yield return null;
    }

    void IsExplodingBullet()
    {

    }

    void IsRotatingBullet()
    {

    }
}
