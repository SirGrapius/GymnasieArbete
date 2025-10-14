using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] public string[] dialogues;
    [SerializeField] public string[] actResponse;
    [SerializeField] public string flavorText; //text that appears automatically
    [SerializeField] public Sprite npcFaceSprite;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Player")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            player.currentNPC = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Player")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            player.currentNPC = null;
        }
    }
}
