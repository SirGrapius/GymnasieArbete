using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackMinigames : MonoBehaviour
{
    [SerializeField] CombatController combatCont;
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] GameObject attackMarker;
    [SerializeField] GameObject currentMarker;
    [SerializeField] float startTime; //the time at which the attack minigame begins
    [SerializeField] float inputTime; //the time at which the player presses the button to finish the minigame
    [SerializeField] float correctTime; //the time at which ending the minigame will result in the most damage
    [SerializeField] float currentTime;
    [SerializeField] Slider mySlider;
    [SerializeField] float difference;
    [SerializeField] float modifier;

    [SerializeField] bool antiBulletHell = false;
    [SerializeField] bool antiInfDamageHell = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        combatCont = GameObject.FindGameObjectWithTag("GameController").GetComponent<CombatController>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (mySlider.value < mySlider.maxValue)
        {
            mySlider.value += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Z) && mySlider.value <= mySlider.maxValue && antiInfDamageHell)
        {
            inputTime = startTime - currentTime;
            mySlider.value = mySlider.maxValue;
            antiInfDamageHell = false;
            StartCoroutine(MinigameOver());
        }
        else if (mySlider.value == mySlider.maxValue && antiInfDamageHell)
        {
            inputTime = 100;
            antiInfDamageHell = false;
            StartCoroutine(MinigameOver());
        }
    }

    IEnumerator MinigameOver()
    {
        difference = Mathf.Abs(correctTime - inputTime);
        modifier = (difference) * playerScript.damageModifier;
        Destroy(currentMarker);
        combatCont.PlayerDealsDamage(modifier);
        if (antiBulletHell == false)
        {
            StartCoroutine(combatCont.EndTurn());
            antiBulletHell = true;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    void StartMinigame()
    {
        correctTime = Random.Range(mySlider.minValue, mySlider.maxValue);
        mySlider.value = correctTime;
        currentMarker = Instantiate(attackMarker, mySlider.handleRect.position, Quaternion.identity);
        mySlider.value = 0;
    }
}
