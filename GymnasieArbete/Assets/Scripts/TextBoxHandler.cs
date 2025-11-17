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
    [SerializeField] NPCScript npc;
    [SerializeField] GameObject currentTextBox;

    [Header("Regular Text Boxes")]
    [SerializeField] GameObject textBoxObject;
    [SerializeField] SpriteRenderer npcFaceRenderer;
    [SerializeField] public string npcName;
    [SerializeField] int currentText;
    [SerializeField] bool textOnScreen;
    [SerializeField] float loadingTime;
    [SerializeField] bool loadingText;

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
        if (textOnScreen)
        {
            TextMeshProUGUI myText = currentTextBox.GetComponent<TextMeshProUGUI>();
            myText.text = npc.dialogues[currentText];
            if (Input.GetKeyDown(KeyCode.Z) && currentText < npc.dialogues.Length - 1 && !loadingText)
            {
                StartCoroutine(LoadTextCoroutine());
                currentText++;
            }
            else if (Input.GetKeyDown(KeyCode.Z) && currentText == npc.dialogues.Length - 1)
            {
                StartCoroutine(EndDialogue(currentTextBox));
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(EndDialogue(currentTextBox));
            }
        }
    }

    public void StartNewDialogue(NPCScript npcInteracted)
    {
        npc = npcInteracted;
        player.currentSpeed = 0;
        currentText = 0;
        currentTextBox = Instantiate(textBoxObject, spawnPos);
        textOnScreen = true;
    }

    void SetFaceSprite(NPCScript npc)
    {
        npcFaceRenderer = textBoxObject.GetComponentInChildren<SpriteRenderer>();
        if (npc.npcFaceSprite == null)
        {
            npcFaceRenderer.color = new Color(0, 0, 0, 0);
        }
        else
        {
            npcFaceRenderer.sprite = npc.npcFaceSprite;
            npcFaceRenderer.color = Color.white;
        }
    }

    IEnumerator LoadTextCoroutine()
    {
        loadingText = true;
        yield return new WaitForSeconds(loadingTime);
        loadingText = false;
        yield return null;
    }

    IEnumerator EndDialogue(GameObject boxToDestroy)
    {
        GameObject currentTextBox = GameObject.FindGameObjectWithTag("TextBox");
        Destroy(currentTextBox);
        currentTextBox = null;
        currentText = 0;
        textOnScreen = false;
        yield return new WaitForSeconds(0.5f);
        player.inDialogue = false;
        player.movementEnabled = true;
        yield return null;
    }

    void ConfirmScreen()
    {

    }
}
