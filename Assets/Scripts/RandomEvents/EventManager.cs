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

    [Header("Trust & Stress Parent (Grid Layout Group)")]
    // Assign the TrustAndStress parent GameObject that holds the GridLayoutGroup
    public RectTransform trustAndStressParent;

    // Assign an empty GameObject sibling placed exactly where you want
    // the bars to move to — Unity will calculate the correct position
    // for any screen size automatically
    public RectTransform choiceTargetPosition;

    private GridLayoutGroup trustAndStressGrid;

    // Original RectTransform values of the parent
    private Vector2 originalParentAnchoredPos;
    private Vector2 originalParentSizeDelta;

    // Original GridLayoutGroup values
    private Vector2 originalCellSize;
    private GridLayoutGroup.Constraint originalConstraint;
    private int originalConstraintCount;

    // Only sizeDelta and cellSize are hardcoded — these are sizes not positions
    // so they don't change between screen sizes
    private readonly Vector2 choiceParentSizeDelta = new Vector2(291.5371f, 291.5371f);
    private readonly Vector2 choiceCellSize        = new Vector2(196.8f, 51.6f);
    private const GridLayoutGroup.Constraint choiceConstraint = GridLayoutGroup.Constraint.FixedRowCount;

    [Header("Bars")]
    public Slider stressBar;
    public Slider trustBar;

    [Header("Time and Day")]
    public GameObject ElapsedTime;
    public Timer TimerReference;
    public TextMeshProUGUI dayText;

    public GameObject StartEventStarter;
    public GameEngine GameEngineReference;

    private EventData currentEvent;
    private Vector2 originalImagePos;

    private bool isMovingImage = false;
    private float moveSpeed = 800f;

    private void Awake()
    {
        // Store original image position
        if (eventImageRect != null)
            originalImagePos = eventImageRect.anchoredPosition;

        // Get GridLayoutGroup from the parent and store all original values
        if (trustAndStressParent != null)
        {
            originalParentAnchoredPos = trustAndStressParent.anchoredPosition;
            originalParentSizeDelta   = trustAndStressParent.sizeDelta;

            trustAndStressGrid = trustAndStressParent.GetComponent<GridLayoutGroup>();

            if (trustAndStressGrid != null)
            {
                originalCellSize        = trustAndStressGrid.cellSize;
                originalConstraint      = trustAndStressGrid.constraint;
                originalConstraintCount = trustAndStressGrid.constraintCount;
            }
        }
    }

    private void Start()
    {
        // Restore bar values from SessionData when Scene 1 loads
        if (SessionData.Instance != null)
        {
            trustBar.value  = SessionData.Instance.Trust;
            stressBar.value = SessionData.Instance.Stress;
        }
    }

    public void TriggerRandomEvent()
    {
        if (allEvents == null || allEvents.Length == 0) return;

        StartEventStarter.SetActive(true);

        currentEvent = allEvents[Random.Range(0, allEvents.Length)];

        titleText.text       = currentEvent.eventTitle;
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
        // Reset event image
        if (eventImageRect != null)
            eventImageRect.anchoredPosition = originalImagePos;

        // Restore parent RectTransform
        if (trustAndStressParent != null)
        {
            trustAndStressParent.anchoredPosition = originalParentAnchoredPos;
            trustAndStressParent.sizeDelta        = originalParentSizeDelta;
        }

        // Restore GridLayoutGroup values
        if (trustAndStressGrid != null)
        {
            trustAndStressGrid.cellSize        = originalCellSize;
            trustAndStressGrid.constraint      = originalConstraint;
            trustAndStressGrid.constraintCount = originalConstraintCount;
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        Choice chosen = currentEvent.choices[choiceIndex];

        stressBar.value = Mathf.Clamp(stressBar.value + chosen.stressChange, 0, 100);
        trustBar.value  = Mathf.Clamp(trustBar.value  + chosen.trustChange,  0, 100);

        bool isPositive = Random.value < chosen.positiveChance;

        if (isPositive)
            trustBar.value  = Mathf.Clamp(trustBar.value  + 5, 0, 100);
        else
            stressBar.value = Mathf.Clamp(stressBar.value + 5, 0, 100);

        // Update SessionData in memory
        if (SessionData.Instance != null)
        {
            SessionData.Instance.UpdateBars(trustBar.value, stressBar.value);
            SessionData.Instance.ElapsedTime = TimerReference.elapsedTime;
            SessionData.Instance.DayAdder    = TimerReference.DayAdder;
            SessionData.Instance.DaysText    = dayText.text;
        }

        // Persist to disk via SaveData
        SaveData.SaveAllGameData(
            trustBar.value, stressBar.value,
            TimerReference.elapsedTime, TimerReference.DayAdder, dayText.text,
            GameEngineReference.MissionTime1, GameEngineReference.MissionTime2,
            GameEngineReference.TimeToTriggerEvent1, GameEngineReference.TimeToTriggerEvent2,
            SessionData.Instance.Mission1Entered, SessionData.Instance.Mission2Entered,
            SessionData.Instance.Event1Triggered, SessionData.Instance.Event2Triggered,
            SessionData.Instance.PlayerPosition, SessionData.Instance.IsMuted
        );

        // Move the parent to the target position read from the scene at runtime
        // instead of hardcoded pixel values — this works correctly on any screen size
        if (trustAndStressParent != null && choiceTargetPosition != null)
        {
            trustAndStressParent.anchoredPosition = choiceTargetPosition.anchoredPosition;
            trustAndStressParent.sizeDelta        = choiceParentSizeDelta;
        }

        // Apply changed GridLayoutGroup values
        if (trustAndStressGrid != null)
        {
            trustAndStressGrid.cellSize        = choiceCellSize;
            trustAndStressGrid.constraint      = choiceConstraint;
            trustAndStressGrid.constraintCount = 2;
        }

        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        if (eventImageRect != null)
        {
            isMovingImage = true;
            Debug.Log("Starting image movement");
        }

        resultPanel.SetActive(true);

        descriptionText.text = isPositive ? chosen.positiveOutcome : chosen.negativeOutcome;

        stressChangeText.text  = (chosen.stressChange >= 0 ? "+" : "") + chosen.stressChange;
        stressChangeText.color = chosen.stressChange >= 0 ? Color.red : Color.green;

        trustChangeText.text  = (chosen.trustChange >= 0 ? "+" : "") + chosen.trustChange;
        trustChangeText.color = chosen.trustChange >= 0 ? Color.green : Color.red;

        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinue);
    }

    private void Update()
    {
        if (isMovingImage && eventImageRect != null)
        {
            Vector2 current = eventImageRect.anchoredPosition;
            current.x -= moveSpeed * Time.unscaledDeltaTime;
            eventImageRect.anchoredPosition = current;

            if (current.x <= -150.15f)
            {
                current.x = -150.15f;
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
        isMovingImage  = false;

        Debug.Log("Event reset");
    }
}