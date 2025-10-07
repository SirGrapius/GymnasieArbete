using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerMovement playerScript;
    [SerializeField] TextBoxHandler dialogueScript;
    [SerializeField] GameObject arena;

    [Header("Fade Settings")]
    [SerializeField] public float fadeDuration;
    [SerializeField] Image myImage;
    
    [Header("Enemies by Area")]
    [SerializeField] GameObject[] trashEnemies;

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

    public IEnumerator FadeInCoroutine()
    {
        Color startColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1);
        Color targetColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0);

        yield return FadeCoroutine(startColor, targetColor);

        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine()
    {
        Color startColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0);
        Color targetColor = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1);

        yield return FadeCoroutine(startColor, targetColor);

        gameObject.SetActive(false);
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
