using System.Collections;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    [Header("Player Stuff")]
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] int playerHP;

    [Header("Enemy Stuff")]
    [SerializeField] EnemyHandler enemyScript;
    [SerializeField] int enemyHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponentInChildren<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EnemyAttackHandler(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);

        yield return null;
    }
}
