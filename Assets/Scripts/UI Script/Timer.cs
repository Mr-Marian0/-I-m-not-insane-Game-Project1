using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI AmPm;
    [SerializeField] TextMeshProUGUI Days;
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

            yield return new WaitForSeconds(1f);

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
