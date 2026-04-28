using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameWin : MonoBehaviour
{
    [Header("Bar References")]
    public Slider stressBar;
    public Slider trustBar;

    [Header("Gameplay References")]
    public Timer timer;
    public JoystickMovement joystickMovement;
    public GameObject pauseButton;
    public GameObject clockObject;
    public GameObject DaysObject;
    public GameObject FixedJoystickObject;
    public GameObject GameEngineObject;
    public GameObject GameOverTextObject;
    public GameObject HalucinationReference;
    public GameObject SButtonReference;
    public Transform playerTransformReference;
    public GameObject ResultLayerReference;
    public GameObject EventObjectIsActive;

    [Header("Endscreen UI")]
    public SpriteRenderer fadeOverlay;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI bodyText;
    public GameObject continueButton;

    [Header("Fade Settings")]
    public float fadeDuration = 0.01f;
    public float typeSpeed = 0.01f;

    [Header("End Messages")]
    public string[] failureMessages = new string[]
    {
        "Your character didn't make it and killed someone.",
        "The character continued his bad habits.",
        "Due to stress, he attempted to escape many times."
    };

    public string[] successMessages = new string[]
    {
        "Congratulation! They released you from asylum.",
        "They think you're totally fine and no problem with you.",
        "You started a new life outside asylum."
    };

    private bool gameOverTriggered;
    private bool trustHit100;
    private bool stressHit100;
    private bool trustReached100First;

    void Start()
    {
        if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(false);
            var color = fadeOverlay.color;
            fadeOverlay.color = new Color(color.r, color.g, color.b, 0f);
        }

        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }

        if (bodyText != null)
        {
            bodyText.text = string.Empty;
        }

        if (headerText != null)
        {
            headerText.text = string.Empty;
        }
    }

    void Update()
    {
        if (gameOverTriggered)
            return;

        if (!trustHit100 && trustBar != null && trustBar.value >= 100f && !EventObjectIsActive.activeSelf)
        {
            trustHit100 = true;
            trustReached100First = !stressHit100;

            if (!stressHit100)
            {
                TriggerEndGame(true);
                return;
            }
        }

        if (!stressHit100 && stressBar != null && stressBar.value >= 100f && !EventObjectIsActive.activeSelf)
        {
            stressHit100 = true;
            bool trustVictory = trustHit100 && trustReached100First;
            TriggerEndGame(trustVictory);
        }
    }

    public void TriggerEndGame(bool trustVictory)
    {
        if (gameOverTriggered)
            return;

        gameOverTriggered = true;
        GameOverTextObject.SetActive(true);
        DisableGameplay();
        StartCoroutine(RunEndGameSequence(trustVictory));
    }

    private void DisableGameplay()
    {
        Time.timeScale = 0f;

        timer.StopTimer = true;
        
        if (ResultLayerReference != null) ResultLayerReference.SetActive(true);
        if(playerTransformReference != null) playerTransformReference.position = new Vector3(-0.05f, -3.41f, 0f);
        if (SButtonReference != null) SButtonReference.SetActive(false);
        if (timer != null) timer.enabled = false;
        if (HalucinationReference != null) HalucinationReference.SetActive(false);
        if (joystickMovement != null) joystickMovement.enabled = false;
        if (pauseButton != null) pauseButton.SetActive(false);
        if (FixedJoystickObject != null) FixedJoystickObject.SetActive(false);
        if (GameEngineObject != null) GameEngineObject.SetActive(false);
    }

    private IEnumerator RunEndGameSequence(bool trustVictory)
    {
        if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(true);
            var targetColor = trustVictory ? Color.white : Color.black;
            targetColor.a = 1f;
            yield return StartCoroutine(FadeOverlay(targetColor));
        }

        if (headerText != null)
        {
            headerText.text = "Game Over";
            headerText.color = trustVictory ? Color.black : Color.white;
        }

        string chosenMessage = GetEndMessage(trustVictory);
        string dayText = GetDaysText();
        string finalMessage = string.IsNullOrEmpty(dayText)
            ? chosenMessage
            : $"On day {dayText}, {chosenMessage}";

        if (bodyText != null)
        {
            bodyText.color = trustVictory ? Color.black : Color.white;
            if (trustVictory)
            {
                yield return StartCoroutine(FadeInText(bodyText, finalMessage));
            }
            else
            {
                yield return StartCoroutine(TypeWriterText(bodyText, finalMessage));
            }
        }

        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }
    }

    private IEnumerator FadeOverlay(Color targetColor)
    {
        if (fadeOverlay == null)
            yield break;

        float elapsed = 0f;
        Color startColor = fadeOverlay.color;
        startColor.a = 0f;
        fadeOverlay.color = startColor;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            fadeOverlay.color = Color.Lerp(startColor, targetColor, Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }

        fadeOverlay.color = targetColor;
    }

    private IEnumerator TypeWriterText(TextMeshProUGUI textMesh, string message)
    {
        textMesh.text = string.Empty;
        for (int i = 0; i < message.Length; i++)
        {
            textMesh.text += message[i];
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
    }

    private IEnumerator FadeInText(TextMeshProUGUI textMesh, string message)
    {
        textMesh.text = message;
        Color targetColor = textMesh.color;
        targetColor.a = 0f;
        textMesh.color = targetColor;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            var color = textMesh.color;
            color.a = alpha;
            textMesh.color = color;
            yield return null;
        }

        var finalColor = textMesh.color;
        finalColor.a = 1f;
        textMesh.color = finalColor;
    }

    private string GetEndMessage(bool trustVictory)
    {
        var messages = trustVictory ? successMessages : failureMessages;
        if (messages == null || messages.Length == 0)
            return trustVictory ? "You have been released from asylum." : "Stress broke your character.";

        int index = Random.Range(0, messages.Length);
        return messages[index];
    }

    private string GetDaysText()
    {
        if (timer != null)
        {
            return timer.DayAdder.ToString();
        }

        return string.Empty;
    }
}

