using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class EventManager : MonoBehaviour
{
    [Header("Events")]
    public EventData[] allEvents;                 // ← This is back where it should be

    [Header("Main Event References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image eventImage;
    public RectTransform eventImageRect;

    [Header("Buttons")]
    public Button[] choiceButtons;
    public Button continueButton;

    [Header("Result Panel")]
    public GameObject resultPanel;
    public TextMeshProUGUI stressChangeText;
    public TextMeshProUGUI trustChangeText;
    
    //TRUST RECT TRANSFORM POSITIONS - REPLACE TO CONGRATULATION - POSITIONS
    public RectTransform MoveTrustPosition;
    public Vector3 TrustDefaultPosXY;

    //STRESS RECT TRANSFORM POSITIONS - REPLACE TO CONGRATULATION - POSITIONS
    public RectTransform MoveStressPosition;
    public Vector3 StressDefaultPosXY;

    [Header("Bars")]
    public Slider stressBar;
    public Slider trustBar;

    public GameObject StartEventStarter;

    private EventData currentEvent;
    private Vector2 originalImagePosition;

    private void Awake()
    {
        if (eventImageRect != null)
            originalImagePosition = eventImageRect.anchoredPosition;
    }

    public void TriggerRandomEvent()
    {
        if (allEvents == null || allEvents.Length == 0)
        {
            Debug.LogError("EventManager: No events assigned in 'allEvents' array!");
            return;
        }

        StartEventStarter.SetActive(true);

        currentEvent = allEvents[Random.Range(0, allEvents.Length)];

        titleText.text = currentEvent.eventTitle;
        descriptionText.text = currentEvent.eventDescription;

        if (eventImageRect != null)
            eventImageRect.anchoredPosition = originalImagePosition;

        if (eventImage != null && currentEvent.eventImage != null)
        {
            eventImage.sprite = currentEvent.eventImage;
            eventImage.gameObject.SetActive(true);
        }

        // Show the 3 choices
        for (int i = 0; i < 3 && i < currentEvent.choices.Count; i++)
        {
            int index = i;
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choices[i].choiceText;
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => MakeChoice(index));
        }

        continueButton.gameObject.SetActive(false);
        resultPanel.SetActive(false);

        Debug.Log("Event started: " + currentEvent.eventTitle);
    }

    public void MakeChoice(int choiceIndex)
    {
        Choice chosen = currentEvent.choices[choiceIndex];

        stressBar.value = Mathf.Clamp(stressBar.value + chosen.stressChange, 0, 100);
        trustBar.value = Mathf.Clamp(trustBar.value + chosen.trustChange, 0, 100);

        MoveTrustPosition.anchoredPosition = new Vector2(-638f, 606f);
        MoveStressPosition.anchoredPosition = new Vector2(1332f, -341f);

        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        if (eventImageRect != null)
            StartCoroutine(MoveImageToLeft());

        resultPanel.SetActive(true);

        bool isPositive = Random.value < chosen.positiveChance;
        descriptionText.text = isPositive ? chosen.positiveOutcome : chosen.negativeOutcome;

        stressChangeText.text = (chosen.stressChange >= 0 ? "+" : "") + chosen.stressChange;
        stressChangeText.color = chosen.stressChange >= 0 ? Color.red : Color.green;

        trustChangeText.text = (chosen.trustChange >= 0 ? "+" : "") + chosen.trustChange;
        trustChangeText.color = chosen.trustChange >= 0 ? Color.green : Color.red;

        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinue);
    }

    private IEnumerator MoveImageToLeft()
    {
        if (eventImageRect == null) yield break;

        float duration = 0.6f;
        Vector2 startPos = eventImageRect.anchoredPosition;
        Vector2 targetPos = new Vector2(-516.71f, startPos.y);

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            eventImageRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        eventImageRect.anchoredPosition = targetPos;
    }

    public void OnContinue()
    {
        StartCoroutine(FadeOutAndReset());
    }

    private IEnumerator FadeOutAndReset()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            cg.alpha = 1f - (t / 0.5f);
            yield return null;
        }

        cg.alpha = 0f;

        resultPanel.SetActive(false);
        if (eventImage != null) eventImage.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        StartEventStarter.SetActive(false);

        if (eventImageRect != null)
            eventImageRect.anchoredPosition = originalImagePosition;

        cg.alpha = 1f;
        Time.timeScale = 1f;
    }
}