using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [Header("Events")]
    public EventData[] allEvents;

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

    public RectTransform MoveTrustPosition;
    public RectTransform MoveStressPosition;

    [Header("Bars")]
    public Slider stressBar;
    public Slider trustBar;

    public GameObject StartEventStarter;

    private EventData currentEvent;
    private Vector2 originalImagePos;
    private Vector2 originalTrustPos;
    private Vector2 originalStressPos;

    private bool isMovingImage = false;
    private float moveSpeed = 800f;        // Adjust this value (higher = faster)

    private void Awake()
    {
        if (eventImageRect != null) originalImagePos = eventImageRect.anchoredPosition;
        if (MoveTrustPosition != null) originalTrustPos = MoveTrustPosition.anchoredPosition;
        if (MoveStressPosition != null) originalStressPos = MoveStressPosition.anchoredPosition;
    }

    public void TriggerRandomEvent()
    {
        if (allEvents == null || allEvents.Length == 0) return;

        StartEventStarter.SetActive(true);

        currentEvent = allEvents[Random.Range(0, allEvents.Length)];

        titleText.text = currentEvent.eventTitle;
        descriptionText.text = currentEvent.eventDescription;

        ResetAllPositions();

        if (eventImage != null && currentEvent.eventImage != null)
        {
            eventImage.sprite = currentEvent.eventImage;
            eventImage.gameObject.SetActive(true);
        }

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
    }

    private void ResetAllPositions()
    {
        if (eventImageRect != null) eventImageRect.anchoredPosition = originalImagePos;
        if (MoveTrustPosition != null) MoveTrustPosition.anchoredPosition = originalTrustPos;
        if (MoveStressPosition != null) MoveStressPosition.anchoredPosition = originalStressPos;
    }

    public void MakeChoice(int choiceIndex)
    {
    Choice chosen = currentEvent.choices[choiceIndex];

    // Apply changes
    stressBar.value = Mathf.Clamp(stressBar.value + chosen.stressChange, 0, 100);
    trustBar.value = Mathf.Clamp(trustBar.value + chosen.trustChange, 0, 100);

    // Save to file
    SaveData.SavePlayer(trustBar.value, stressBar.value);

    // Move UI positions
    if (MoveTrustPosition != null)
        MoveTrustPosition.anchoredPosition = new Vector2(-638.6f, 739.4f);

    if (MoveStressPosition != null)
        MoveStressPosition.anchoredPosition = new Vector2(1329f, -475f);

    foreach (var btn in choiceButtons)
        btn.gameObject.SetActive(false);

    // Start image movement
    if (eventImageRect != null)
    {
        isMovingImage = true;
        Debug.Log("Starting image movement");
    }

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

    private void Update()
    {
        if (isMovingImage && eventImageRect != null)
        {
            // Move left every frame using real time
            Vector2 current = eventImageRect.anchoredPosition;
            current.x -= moveSpeed * Time.unscaledDeltaTime;

            eventImageRect.anchoredPosition = current;

            // Stop when it reaches target
            if (current.x <= -516.71f)
            {
                current.x = -516.71f;
                eventImageRect.anchoredPosition = current;
                isMovingImage = false;
                Debug.Log("Image reached left position");
            }
        }
    }

    public void OnContinue()
    {
        Debug.Log("Continue clicked");

        resultPanel.SetActive(false);
        if (eventImage != null) eventImage.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        StartEventStarter.SetActive(false);

        ResetAllPositions();
        Time.timeScale = 1f;

        isMovingImage = false;   // Stop movement if still running

        Debug.Log("Event reset");
    }
}