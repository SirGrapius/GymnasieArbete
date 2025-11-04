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

    [Header("Dialogue")]
    [SerializeField] TextBoxHandler dialogueScript;

    [Header("Player Stuff")]
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] Button[] buttons;
    [SerializeField] bool buttonsInteractable;
    [SerializeField] GameObject confirmScreen;

    [Header("Player Attack")]
    [SerializeField] GameObject minigameSpawn;
    [SerializeField] GameObject minigameObject;
    [SerializeField] GameObject marker;
    [SerializeField] float startTime; //the time at which the attack minigame begins
    [SerializeField] float inputTime; //the time at which the player presses the button to finish the minigame
    [SerializeField] float correctTime; //the time at which ending the minigame will result in the most damage
    [SerializeField] Slider minigameSlider;

    [Header("Enemy Stuff")]
    [SerializeField] EnemyHandler enemyScript;
    [SerializeField] float mercyValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gameManager = GetComponent<GameManager>();
        dialogueScript.npcName = enemyScript.gameObject.name;
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
    }

    public void StartCombat()
    {
        //enemyScript == GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHandler>();
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
        bool pressed = false;
        float currentTime = 0;
        float difference;
        float modifier;
        GameObject myMarker;
        GameObject myMinigame;

        myMinigame = Instantiate(minigameObject, minigameSpawn.transform);
        minigameSlider = myMinigame.GetComponent<Slider>();

        correctTime = Random.Range(minigameSlider.minValue, minigameSlider.maxValue);
        minigameSlider.value = correctTime;
        myMarker = Instantiate(marker, minigameSlider.handleRect.position, Quaternion.identity);
        minigameSlider.value = 0;

        if (minigameSlider.value < minigameSlider.maxValue)
        {
            minigameSlider.value = minigameSlider.value + Time.deltaTime;
            currentTime = minigameSlider.value;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            inputTime = startTime - currentTime;
            minigameSlider.value = 0;
            Destroy(myMarker);
        }

        yield return new WaitForSeconds(minigameSlider.maxValue + 0.25f);

        if (myMarker != null)
        {
            inputTime = 100;
            Destroy(myMarker);
        }
        difference = Mathf.Abs(correctTime - inputTime);
        modifier = difference * playerScript.damageModifier;
        enemyScript.hitPoints -= Mathf.RoundToInt(playerScript.baseDamage * modifier);
        StartCoroutine(EnemyAttackHandler(5));
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
        StartCoroutine(enemyScript.AttackCorutine(20));
        yield return new WaitForSeconds(attackDuration);
        enemyAttacking = false;
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
