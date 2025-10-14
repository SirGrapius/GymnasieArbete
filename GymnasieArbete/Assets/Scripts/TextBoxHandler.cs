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
    [SerializeField] SpriteRenderer npcFaceRenderer;
    [SerializeField] public string npcName;
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

    public void StartNewDialogue(NPCScript npc)
    {
        player.currentSpeed = 0;
        currentText = 0;
        Instantiate(textBoxObject, spawnPos);
        StartCoroutine(DialogueCoroutine(1, npc));
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

    IEnumerator DialogueCoroutine(float loadingTime, NPCScript npc)
    {
        SetFaceSprite(npc);
        bool loadingText = false;
        for (int i = 0; i <= npc.dialogues.Length;)
        {
            Text textBoxText = textBoxObject.GetComponent<Text>();
            textBoxText.text = npc.dialogues[i];
            if (Input.anyKeyDown && !loadingText)
            {
                i++;
                loadingText = true;
                yield return new WaitForSeconds(loadingTime);
                loadingText = false;
                if (i == npc.dialogues.Length)
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
