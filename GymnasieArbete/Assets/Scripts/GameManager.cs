using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerMovement playerScript;
    [SerializeField] TextBoxHandler dialogueScript;
    [SerializeField] public GameObject arena;

    [Header("Fade Settings")]
    [SerializeField] public float fadeDuration;
    [SerializeField] Image myImage;
    
    [Header("Enemies by Area")]
    [SerializeField] GameObject[] trashEnemies;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(StartCombat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartCombat()
    {
        StartCoroutine(playerScript.EnterCombat());
        yield return null;
    }

    public void PlayerIsDead()
    {

        ResetToSave();
    }

    void ResetToSave()
    {
        
    }

    public IEnumerator FadeInCoroutine()
    {
        myImage.gameObject.SetActive(true);

        Color startColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1);
        Color targetColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0);

        yield return FadeCoroutine(startColor, targetColor);

        myImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine()
    {
        myImage.gameObject.SetActive(true);

        Color startColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0);
        Color targetColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1);

        yield return FadeCoroutine(startColor, targetColor);

        myImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeCoroutine(Color startColor, Color targetColor)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / fadeDuration;
            myImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
