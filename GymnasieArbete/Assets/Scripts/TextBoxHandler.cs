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
    [SerializeField] public string npcName;
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

    void TestFunction()
    {
        StartNewDialogue(texts, false);
    }

    void NPCDialogues()
    {
        switch (npcName)
        {
            case "Mr_Peeks":
                {
                    texts[0] = "Mr. Peeks blocks your path."; //flavour text upon battle starting
                    texts[1] = "Mr. Peeks stares with joy."; //mercy action 1 compliment
                    texts[2] = "Mr. Peeks revels in victory."; //mercy action 2 roleplay
                    texts[3] = "Mr. Peeks accepts the gift with glee."; //mercy action 3 gift

                    break;
                }
        }
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
