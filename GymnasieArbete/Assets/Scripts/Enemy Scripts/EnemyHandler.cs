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
    [SerializeField] int[] projectileAmountPerAttack;
    [SerializeField] float minAttack;
    [SerializeField] float maxAttack;
    [SerializeField] GameObject[] spawnFields;

    [Header("Stat Settings")]
    [SerializeField] public int hitPoints;
    [SerializeField] public int experienceReward;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator AttackCorutine()
    {
        int whatAttack;
        int spawnLane;

        Vector3[] v = new Vector3[4];

        whatAttack = Mathf.RoundToInt(Random.Range(minAttack, maxAttack));

        for (int i = 0; i < projectileAmountPerAttack[whatAttack]; i++) //spawns an amount of enemy equal to the amount of enemy points
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            spawnLane = Mathf.RoundToInt(Random.Range(0, 3)); //decides what lane the enemy will spawn on
            BoxCollider2D laneCollider = spawnFields[spawnLane].GetComponent<BoxCollider2D>();
            float topSide = laneCollider.size.y + spawnFields[spawnLane].transform.position.y;
            float bottomSide = -laneCollider.size.y + spawnFields[spawnLane].transform.position.y;
            float rightSide = laneCollider.size.x + spawnFields[spawnLane].transform.position.x;
            float leftSide = -laneCollider.size.x + spawnFields[spawnLane].transform.position.x;
            Vector3 spawnPos = new Vector3(Random.Range(leftSide, rightSide), Random.Range(bottomSide, topSide), 0);
            Instantiate(projectiles[whatAttack], spawnPos, Quaternion.identity);
        }
        yield return null;
    }
}
