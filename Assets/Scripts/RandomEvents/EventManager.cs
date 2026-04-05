using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public EventData[] allEvents;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public Button[] choiceButtons;     // Your 3 choice buttons
    public Button continueButton;      // Add one "Continue" button

    public Slider stressBar;
    public Slider trustBar;

    public GameObject StartEventStarter;

    private EventData currentEvent;

    public void TriggerRandomEvent()
    {
        StartEventStarter.SetActive(true);

        currentEvent = allEvents[Random.Range(0, allEvents.Length)];

        titleText.text = currentEvent.eventTitle;
        descriptionText.text = currentEvent.eventDescription;

        // Show 3 choices
        for (int i = 0; i < 3 && i < currentEvent.choices.Count; i++)
        {
            int index = i;
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choices[i].choiceText;
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => MakeChoice(index));
        }

        continueButton.gameObject.SetActive(false);
    }

    public void MakeChoice(int choiceIndex)
    {
        Choice chosen = currentEvent.choices[choiceIndex];

        // Apply effects from choice
        stressBar.value = Mathf.Clamp(stressBar.value + chosen.stressChange, 0, 100);
        trustBar.value  = Mathf.Clamp(trustBar.value  + chosen.trustChange,  0, 100);

        // Hide choice buttons
        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        // Show positive or negative outcome
        float roll = Random.value;
        bool isPositive = roll < chosen.positiveChance;

        if (isPositive)
        {
            descriptionText.text = chosen.positiveOutcome;
        }
        else
        {
            descriptionText.text = chosen.negativeOutcome;
        }

        // Show Continue button
        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinue);
    }

    public void OnContinue()
    {
        continueButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        StartEventStarter.SetActive(false);
        // Go back to waiting / idle state in your room
        // You can call TriggerRandomEvent() again after some time, or show "waiting..." text
    }
}