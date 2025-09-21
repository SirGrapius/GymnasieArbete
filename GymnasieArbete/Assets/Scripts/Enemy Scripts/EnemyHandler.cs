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
    [SerializeField] int hitPoints;


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

        Vector3 spawnPos;

        int projectileAmount;
        projectileAmount = projectileAmountPerAttack[attackToDo];

        for (int i = 0; i < projectileAmount; i++)
        {

        }
        yield return null;
    }
}
