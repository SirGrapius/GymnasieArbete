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
        int attackToDo = Mathf.RoundToInt(Random.Range(minAttack, maxAttack));

        Vector3 spawnPos = new Vector3(0, 0, 0); //value is temporary to avoid an error, randomize later

        int projectileAmount;
        projectileAmount = projectileAmountPerAttack[attackToDo];

        for (int i = 0; i < projectileAmount; i++)
        {
            Instantiate(projectiles[attackToDo], spawnPos, Quaternion.identity);
        }
        yield return null;
    }
}
