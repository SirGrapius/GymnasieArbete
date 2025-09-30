using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    [SerializeField] bool enemyAttacking;
    [SerializeField] bool textOnScreen;
    [SerializeField] GameManager gameManager;

    [Header("Text Stuff")]
    [SerializeField] GameObject textBox;
    [SerializeField] GameObject currentTextBox;

    [Header("Durations & Times")]
    [SerializeField] float attackStartUp;

    [Header("Player Stuff")]
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] Button[] buttons;
    [SerializeField] bool buttonsInteractable;
    [SerializeField] GameObject confirmScreen;

    [Header("Enemy Stuff")]
    [SerializeField] EnemyHandler enemyScript;
    [SerializeField] int enemyHP;
    [SerializeField] float mercyValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textOnScreen)
        {
            buttonsInteractable = false;
            if (Input.anyKeyDown)
            {
                Destroy(currentTextBox);
                textOnScreen = false;
            }
        }

        if (!buttonsInteractable)
        {
            buttons[0 & 1 & 2 & 3].interactable = false;
        }
        else
        {
            buttons[0 & 1 & 2 & 3].interactable = true;
        }
    }

    public void AttackButton()
    {
        //first a confirm screen, then start attack minigame.
    }

    public void ActionButton()
    {
        //brings up a list of actions, certain actions deal damage, others buff you, others increase mercy value
    }

    public void ItemsButton()
    {
        //opens the players inventory allowing them to use an item to restore health or whatever
    }

    public void MercyButton()
    {
        //if mercy value is high enough spares enemy otherwise does NOTHING
        
    }

    IEnumerator DoPlayerAttack()
    {

        yield return null;
    }

    IEnumerator DoAction()
    {

        yield return null;
    }

    IEnumerator DoItems()
    {

        yield return null;
    }

    IEnumerator DoMercy()
    {

        yield return null;
    }

    IEnumerator PlayerDoesSomething(string text, int whatButtonWasPressed)
    {
        currentTextBox = Instantiate(textBox, this.transform.position, Quaternion.identity);
        TextMeshPro textField = currentTextBox.GetComponent<TextMeshPro>();
        textField.text = text;

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

    void EndCombat()
    {
        if (playerScript.hitPoints <= 0)
        {
            gameManager.PlayerIsDead();
        }
        if (enemyHP <= 0)
        {
            playerScript.experience += enemyScript.experienceReward;
        }
        playerScript.LeaveCombat();
    }
}
