using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] Transform playerTransform;

    [SerializeField] float speed;

    [Header("Hit Effects")]
    [SerializeField] public int damage;
    [SerializeField] public float immunityDuration;

    [Header("Bullet Types")]
    [SerializeField] bool regularBullet;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    void Start()
    {
        if (regularBullet)
        {
            IsRegularBullet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowards(playerTransform.position);
    }

    void IsRegularBullet()
    {
        RotateTowards(playerTransform.position);
        myRigidBody.linearVelocity = transform.up * speed;
    }

    void RotateTowards(Vector2 target)
    {
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg);
    }
}
