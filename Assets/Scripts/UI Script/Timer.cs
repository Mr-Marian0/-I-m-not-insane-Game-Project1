using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI AmPm;
    [SerializeField] TextMeshProUGUI Days;
    [SerializeField] GameObject DayCountUI;
    [SerializeField] float fadeInSpeed = 1f;
    [SerializeField] float textDelay = 3f;
    public int DayAdder = 1;
    public int AM = 1;
    public int PM = 0;
    public int minutes;
    public int seconds;
    public GameEngine gameEngineReference;

    [SerializeField] public float elapsedTime;
    public int FastForward = 8;

    public float waitMin = 1f;
    public float waitMax = 3f;

    void Start()
{
    StartCoroutine(UpdateTimeRandomly());
}

public void TriggerDayCount(int nextDay)
{
    StartCoroutine(ShowDayCount(nextDay));
}

public IEnumerator ShowDayCount(int nextDay)
{
    Time.timeScale = 0;

    DayCountUI.SetActive(true);
    CanvasGroup cg = DayCountUI.GetComponent<CanvasGroup>();
    if (cg != null)
    {
        cg.alpha = 0f;
        while (cg.alpha < 1f)
        {
            cg.alpha += Time.unscaledDeltaTime * fadeInSpeed;
            yield return null;
        }
    }

    TextMeshProUGUI dayText = DayCountUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    if (dayText != null)
    {
        dayText.text = "Day";
        yield return new WaitForSecondsRealtime(textDelay);
        dayText.text = "Day " + nextDay;
        yield return new WaitForSecondsRealtime(textDelay);
    }

    DayCountUI.SetActive(false);
    Time.timeScale = 1;
}

IEnumerator UpdateTimeRandomly()
{
    while (true)
    {
        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));

        // Advance time
        elapsedTime += Random.Range(30f, 40f);

        minutes = Mathf.FloorToInt(elapsedTime / 60f);
        seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Skip minutes 30–40
        if (minutes >= 30 && minutes < 40)
        {
            int extraSkip = 40 - minutes;
            minutes += extraSkip;

            elapsedTime = minutes * 60f + seconds;
        }

        // 24-hour format (HH:mm)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Check if we reached or exceeded 24 hours
        if (minutes >= 24)
        {
            // Force exact 24:00 display
            minutes = 24;
            seconds = 0;
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return StartCoroutine(ShowDayCount(DayAdder + 1));

            // Reset to next day
            gameEngineReference.ResetFlagEventTriggered();
            DayAdder += 1;
            elapsedTime = 0;
            Days.text = "DAY " + DayAdder;

            continue;
        }
    }
}
}
