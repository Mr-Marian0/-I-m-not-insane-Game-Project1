using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartFunctions : MonoBehaviour
{
    public GameObject firstImageObject; // Disabled by default, will enable and fade in
    public GameObject secondImageObject; // Disabled by default, will enable and fade in
    public GameObject thirdObject; // This object will fade out when skipping
    public TextMeshProUGUI SubTitleText; // This object will fade out when skipping
    public float fadeDuration = 1f;
    public float thirdObjectDelay = 1f;
    public Button mainButton; // Reference to the main button to check its text
    public GameObject TitleScreen;
    public GameObject MenuButtonsReference;
    public StoryandMechanics storyAndMechanicsReference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContinueNewGameButton()
    {
        string buttonText = GetButtonText(mainButton);
        if (buttonText == "CONTINUE")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
        else if (buttonText == "NEW GAME")
        {
            StartCoroutine(EnableAndFadeSequence());
            StartCoroutine(CallStartStoryWithDelay(2f));
        }
    }

    IEnumerator CallStartStoryWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        storyAndMechanicsReference.StartStory();
    }


    public void OnSkipButton()
    {
        SubTitleText.gameObject.SetActive(false);
        StartCoroutine(FadeOutSequence());
    }

    public void OnExitButton()
    {
        Application.Quit();
        Debug.Log("Exiting game...");
    }

    public void SetButtonText(Button button, string text)
    {
        TextMeshProUGUI tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = text;
            return;
        }

        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = text;
        }
    }

    private string GetButtonText(Button button)
    {
        TextMeshProUGUI tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            return tmpText.text;
        }

        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            return buttonText.text;
        }
        return "";
    }

    private IEnumerator EnableAndFadeSequence()
    {
        if (firstImageObject != null)
        {
            firstImageObject.SetActive(true);
            yield return StartCoroutine(FadeInImage(firstImageObject, fadeDuration));
        }

        if (secondImageObject != null)
        {
            secondImageObject.SetActive(true);
            yield return StartCoroutine(FadeInImage(secondImageObject, fadeDuration));
        }

        if (thirdObject != null)
        {
            yield return new WaitForSeconds(thirdObjectDelay);
            thirdObject.SetActive(true);
        }
    }

    private IEnumerator FadeInImage(GameObject targetObject, float duration)
    {
        Image image = targetObject.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogWarning($"StartFunctions: GameObject '{targetObject.name}' does not have an Image component.");
            yield break;
        }

        Color color = image.color;
        color.a = 0f;
        image.color = color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            image.color = color;
            yield return null;
        }

        TitleScreen.SetActive(false);
        MenuButtonsReference.SetActive(false);

        color.a = 1f;
        image.color = color;
    }

    private IEnumerator FadeOutSequence()
    {
        if (thirdObject != null && thirdObject.activeSelf)
        {
            thirdObject.SetActive(false);
        }

        if (secondImageObject != null && secondImageObject.activeSelf)
        {
            yield return StartCoroutine(FadeOutImage(secondImageObject, fadeDuration));
            secondImageObject.SetActive(false);
        }

        if (firstImageObject != null && firstImageObject.activeSelf)
        {
            yield return StartCoroutine(FadeOutImage(firstImageObject, fadeDuration));
            firstImageObject.SetActive(false);
        }
        
        // After fading out, load the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator FadeOutImage(GameObject targetObject, float duration)
    {
        
        Image image = targetObject.GetComponent<Image>();
        if (image == null)
        {
            yield break;
        }

        Color color = image.color;
        float startAlpha = color.a;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }
}
