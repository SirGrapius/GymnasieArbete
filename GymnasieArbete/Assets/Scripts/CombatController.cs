using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] float attackStartUp; //enemy attack start up time

    [Header("Dialogue")]
    [SerializeField] TextBoxHandler dialogueScript;

    [Header("Player Stuff")]
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] Button[] buttons;
    [SerializeField] bool buttonsInteractable;
    [SerializeField] GameObject confirmScreen;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    string hpInText;

    [Header("Player Attack")]
    [SerializeField] GameObject minigameSpawn; //spawn pos for attack minigame
    [SerializeField] GameObject minigameObject; //attack minigame
    [SerializeField] GameObject marker; //marker showing when to click
    [SerializeField] Slider minigameSlider; //attack minigame slider

    [Header("Enemy Stuff")]
    [SerializeField] EnemyHandler enemyScript;
    [SerializeField] float mercyValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gameManager = GetComponent<GameManager>();
        dialogueScript.npcName = enemyScript.gameObject.name;
        healthBar.maxValue = playerScript.maxHitPoints;
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

        if (enemyScript.hitPoints <= 0)
        {
            WinCombat();
        }
        if (playerScript.hitPoints <= 0)
        {
            LoseCombat();
        }
        healthBar.value = playerScript.hitPoints;
        healthText.text = healthBar.value.ToString();
    }

    public void StartCombat()
    {
        enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHandler>();
        StartTurn();
    }

    public void AttackButton()
    {
        StartCoroutine(DoPlayerAttack());
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
        buttonsInteractable = false;
        GameObject myMinigame;

        myMinigame = Instantiate(minigameObject, minigameSpawn.transform);
        minigameSlider = myMinigame.GetComponent<Slider>();

        yield return null;
    }

    IEnumerator DoAction()
    {

        EndTurn();
        yield return null;
    }

    IEnumerator DoItems()
    {

        EndTurn();
        yield return null;
    }

    IEnumerator DoMercy()
    {

        EndTurn();
        yield return null;
    }

    IEnumerator PlayerDoesSomething(string text, int whatButtonWasPressed)
    {
        currentTextBox = Instantiate(textBox, this.transform.position, Quaternion.identity);
        TextMeshPro textField = currentTextBox.GetComponent<TextMeshPro>();
        textField.text = text;

        yield return null;
    }

    public IEnumerator EnemyAttackHandler()
    {
        int whatAttack = Mathf.RoundToInt(Random.Range(enemyScript.minAttack, enemyScript.maxAttack));
        enemyAttacking = true;
        yield return new WaitForSeconds(attackStartUp);
        StartCoroutine(enemyScript.AttackCorutine(whatAttack));
        yield return new WaitForSeconds(enemyScript.attackDurations[whatAttack]+5);
        enemyAttacking = false;
        StartTurn();
        yield return null;
    }

    public void PlayerDealsDamage(float modifier)
    {
        enemyScript.hitPoints -= Mathf.RoundToInt(playerScript.baseDamage * modifier);
    }

    public void StartTurn() //starts the players turn, letting them make an action
    {
        playerScript.movementEnabled = false;
        buttonsInteractable = true;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length - 1; i++)
        {
            Destroy(bullets[i]);
        }
    }

    public IEnumerator EndTurn() //ends the players turn after they perform an action and starts the enemy's attack
    {
        playerScript.movementEnabled = true;
        buttonsInteractable = false;
        Debug.Log("what");
        StartCoroutine(EnemyAttackHandler());
        yield return null;
    }

    void WinCombat()
    {
        playerScript.experience += enemyScript.experienceReward;
        StartCoroutine(playerScript.LeaveCombat());
    }

    void LoseCombat()
    {

        gameManager.PlayerIsDead();
        StartCoroutine(playerScript.LeaveCombat());
    }
}
