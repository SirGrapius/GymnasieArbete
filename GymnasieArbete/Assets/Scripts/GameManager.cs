using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerMovement playerScript;

    void Start()
    {
        playerScript = GetComponentInChildren<PlayerMovement>();
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
}
