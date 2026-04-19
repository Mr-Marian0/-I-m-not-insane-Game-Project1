using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public TextMeshProUGUI Days;
    [SerializeField] GameObject DayCountUI;
    [SerializeField] float fadeInSpeed = 1f;
    [SerializeField] float textDelay = 3f;
    public int DayAdder = 1;
    public int minutes;
    public int seconds;
    public GameEngine gameEngineReference;

    [SerializeField] public float elapsedTime;
    public int FastForward = 8;

    public float waitMin = 1f;
    public float waitMax = 3f;

    void Start()
    {
        bool restored = false;

        if (SessionData.Instance != null && SessionData.Instance.ElapsedTime > 0f && SessionData.Instance.NewGame == false)
        {
            elapsedTime = SessionData.Instance.ElapsedTime;
            DayAdder = SessionData.Instance.DayAdder;

            if (Days != null)
                Days.text = SessionData.Instance.DaysText;

            restored = true;
        }
        else if (SaveData.HasSaveFile())
        {
            PlayerData savedData = SaveData.LoadPlayer();
            SessionData.Instance.NewGame = false;
            if (savedData != null)
            {
                elapsedTime = savedData.ElapsedTime;
                DayAdder = savedData.DayAdder > 0 ? savedData.DayAdder : 1;

                if (Days != null)
                    Days.text = string.IsNullOrEmpty(savedData.DaysText) ? "DAY " + DayAdder : savedData.DaysText;

                if (SessionData.Instance != null)
                {
                    SessionData.Instance.ElapsedTime = elapsedTime;
                    SessionData.Instance.DayAdder = DayAdder;
                    SessionData.Instance.DaysText = string.IsNullOrEmpty(savedData.DaysText) ? "DAY " + DayAdder : savedData.DaysText;
                }

                restored = true;
            }
        }

        if (restored)
        {
            minutes = Mathf.FloorToInt(elapsedTime / 60f);
            seconds = Mathf.FloorToInt(elapsedTime % 60f);
            if (timerText != null)
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

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

            elapsedTime += Random.Range(30f, 40f);

            minutes = Mathf.FloorToInt(elapsedTime / 60f);
            seconds = Mathf.FloorToInt(elapsedTime % 60f);

            // Skip minutes 30-40
            if (minutes >= 30 && minutes < 40)
            {
                int extraSkip = 40 - minutes;
                minutes += extraSkip;
                elapsedTime = minutes * 60f + seconds;
            }

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (minutes >= 24)
            {
                minutes = 24;
                seconds = 0;
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                yield return StartCoroutine(ShowDayCount(DayAdder + 1));

                gameEngineReference.ResetFlagEventTriggered();
                DayAdder += 1;
                elapsedTime = 0;
                Days.text = "DAY " + DayAdder;

                // Keep SessionData in sync when a new day starts
                if (SessionData.Instance != null)
                {
                    SessionData.Instance.DayAdder = DayAdder;
                    SessionData.Instance.DaysText = "DAY " + DayAdder;
                    SessionData.Instance.ElapsedTime = 0f;
                }

                continue;
            }
        }
    }
}