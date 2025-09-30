using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextBoxHandler : MonoBehaviour
{
    [Header("References to Other Objects")]
    [SerializeField] PlayerMovement player;
    [SerializeField] CombatController combatController;
    [SerializeField] Transform spawnPos;

    [Header("Regular Text Boxes")]
    [SerializeField] GameObject textBoxObject;
    [SerializeField] string[] texts;
    [SerializeField] int currentText;
    [SerializeField] bool textOnScreen;

    [Header("Confirm Screen")]
    [SerializeField] GameObject confirmScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void StartNewDialogue(string[] dialogues, bool isFlavourText)
    {
        if (isFlavourText)
        {
            player.currentSpeed = 0;
        }
        currentText = 0;
        Instantiate(textBoxObject, spawnPos.position, Quaternion.identity);
        StartCoroutine(DialogueCoroutine(1));
    }

    IEnumerator DialogueCoroutine(float loadingTime)
    {
        bool loadingText = false;
        for (int i = 0; i <= texts.Length;)
        {
            Text textBoxText = textBoxObject.GetComponent<Text>();
            textBoxText.text = texts[i];
            if (Input.anyKeyDown && !loadingText)
            {
                i++;
                loadingText = true;
                yield return new WaitForSeconds(loadingTime);
                loadingText = false;
                if (i == texts.Length)
                {
                    i++;
                    Destroy(textBoxObject);
                    player.currentSpeed = player.baseSpeed;
                }
            }
        }
        yield return null;
    }

    void ConfirmScreen()
    {

    }
}
