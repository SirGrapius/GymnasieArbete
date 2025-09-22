using System.Collections;
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
    [SerializeField] bool autoAimBullet;
    [SerializeField] bool verticalBullet;
    [SerializeField] bool horizontalBullet;
    [SerializeField] bool aoeBullet; //the big warning sign then half the arena gets nuked or whatever
    [SerializeField] bool explodingBullet;
    [SerializeField] bool rotatingBullet;
    

    private void Awake()
    {
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

    IEnumerator HasStartUp(float minSpeed, float maxSpeed)
    {

        yield return null;
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

    void IsAoEBullet()
    {

    }

    void IsExplodingBullet()
    {

    }

    void IsRotatingBullet()
    {

    }
}
