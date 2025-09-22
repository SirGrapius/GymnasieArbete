using System.Collections;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] bool enemyAttacking;

    [Header("Durations & Times")]
    [SerializeField] float attackStartUp;

    [Header("Player Stuff")]
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] int playerHP;

    [Header("Enemy Stuff")]
    [SerializeField] EnemyHandler enemyScript;
    [SerializeField] int enemyHP;
    [SerializeField] float mercyValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponentInChildren<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackButton()
    {
        //first a confirm screen, then start attack minigame.
    }

    void ActionButton()
    {
        //brings up a list of actions, certain actions deal damage, others buff you, others increase mercy value
    }

    void MercyButton()
    {
        //if mercy value is high enough spares enemy otherwise does NOTHING
        
    }

    IEnumerator PlayerDoesSomething()
    {
        //check what the player is doing, attacking, acting or sparing, bring up flavour text, do the thing and finally either start the enemy's attack or end battle
        yield return null;
    }

    public IEnumerator EnemyAttackHandler(float attackDuration)
    {
        enemyAttacking = true;
        yield return new WaitForSeconds(attackStartUp);
        StartCoroutine(enemyScript.AttackCorutine());
        yield return new WaitForSeconds(attackDuration);
        enemyAttacking = false;
        yield return null;
    }
}
