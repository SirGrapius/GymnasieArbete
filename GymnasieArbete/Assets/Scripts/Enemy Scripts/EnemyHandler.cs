using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] Transform[] attackSpawnTransforms;

    [Header("Enemy Type Settings")]
    [SerializeField] string enemyName;
    [SerializeField] Sprite[] sprites;

    [Header("Attack Settings")]
    [SerializeField] GameObject[] projectiles;
    [SerializeField] public float[] attackDurations; //number in list should match projectiles list
    [SerializeField] int[] projectileAmountPerAttack;
    [SerializeField] public int minAttack;
    [SerializeField] public int maxAttack;
    [SerializeField] GameObject[] spawnFields; //t,b,l,r

    [Header("Stat Settings")]
    [SerializeField] public int hitPoints;
    [SerializeField] public int experienceReward;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAttack = projectiles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator AttackCorutine(int whatAttack)
    {
        int spawnLane;
        BulletScript bullet = projectiles[whatAttack].GetComponent<BulletScript>();
        if (bullet.autoAimBullet)
        {
            for (int i = 0; i < projectileAmountPerAttack[whatAttack]; i++) //spawns an amount of enemy equal to the amount of enemy points
            {
                spawnLane = Mathf.RoundToInt(Random.Range(0, 3)); //decides what lane the enemy will spawn on
                BoxCollider2D laneCollider = spawnFields[spawnLane].GetComponent<BoxCollider2D>();
                float topSide = laneCollider.size.y + spawnFields[spawnLane].transform.position.y;
                float bottomSide = -laneCollider.size.y + spawnFields[spawnLane].transform.position.y;
                float rightSide = laneCollider.size.x + spawnFields[spawnLane].transform.position.x;
                float leftSide = -laneCollider.size.x + spawnFields[spawnLane].transform.position.x;
                Vector3 spawnPos = new Vector3(Random.Range(leftSide, rightSide), Random.Range(bottomSide, topSide), 0);
                Instantiate(projectiles[whatAttack], spawnPos, Quaternion.identity);
                yield return new WaitForSeconds((attackDurations[whatAttack]) / projectileAmountPerAttack[whatAttack]);
            }
        }
        else if (bullet.aoeBullet)
        {
            for (int i = 0; i < projectileAmountPerAttack[whatAttack]; ++i) //spawns each attack
            {
                for (int j = 0; j < bullet.amountOfAoE; ++j)
                {
                    spawnLane = Mathf.RoundToInt(Random.Range(0, 3)); //decides what lane the enemy will spawn on
                    BulletScript currentBullet = Instantiate(projectiles[whatAttack], spawnFields[spawnLane].transform.position, Quaternion.identity).GetComponent<BulletScript>();
                    if (spawnLane == 0) //if top
                    {
                        currentBullet.gameObject.transform.position = new Vector3(0, 0.75f, 0);
                        currentBullet.spriteRenderer.sprite = currentBullet.sprites[1];
                    }
                    if (spawnLane == 1) //if bottom
                    {
                        currentBullet.gameObject.transform.position = new Vector3(0, -1.2f, 0);
                        currentBullet.spriteRenderer.sprite = currentBullet.sprites[1];
                    }
                    if (spawnLane == 2) //if left
                    {
                        currentBullet.gameObject.transform.position = new Vector3(-2, -0.2f, 0);
                        currentBullet.gameObject.transform.localScale = new Vector3(5, 4, 0);
                        currentBullet.spriteRenderer.sprite = currentBullet.sprites[0];
                    }
                    if (spawnLane == 3)//if right
                    {
                        currentBullet.gameObject.transform.position = new Vector3(2,-0.2f,0);
                        currentBullet.gameObject.transform.localScale = new Vector3(5,4,0);
                        currentBullet.spriteRenderer.sprite = currentBullet.sprites[0];
                    }
                }
                yield return new WaitForSeconds(bullet.startUpTime + bullet.timeBetween);
            }
        }
            yield return null;
    }

    public void TakeDamage(int damageTaken)
    {
        hitPoints -= damageTaken;
    }
}
