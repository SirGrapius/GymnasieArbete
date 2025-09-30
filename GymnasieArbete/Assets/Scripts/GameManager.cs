using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerMovement playerScript;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartCombat()
    {
        playerScript.EnterCombat();
        yield return null;
    }

    public void PlayerIsDead()
    {

        ResetToSave();
    }

    void ResetToSave()
    {
        
    }
}
