using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SessionData : MonoBehaviour
{
    public static SessionData Instance;

    [Header("Bar Values")]
    public float Trust = 99f;
    public float Stress;

    [Header("Player Position")]
    public Vector3 PlayerPosition = new Vector3(-0.05f, -2.91f, 0);

    [Header("Bars")]
    public Slider stressBar;
    public Slider trustBar;

    [Header("Timer")]
    public float ElapsedTime = 0f;
    public int DayAdder = 1;
    public string DaysText = "DAY 1";
    public TextMeshProUGUI dayText;

    [Header("GameObject References")]
    public GameEngine GameEngineReference;
    public Timer TimerReference;

    [Header("Audio")]
    public bool IsMuted = false;
    public int MissionTime1 = 0;
    public int MissionTime2 = 0;
    public int TimeToTriggerEvent1 = 0;
    public int TimeToTriggerEvent2 = 0;
    public bool Mission1Entered = false;
    public bool Mission2Entered = false;
    public bool Event1Triggered = false;
    public bool Event2Triggered = false;
    public bool NewGame = false;

    private PlayerProgress playerProgress;
    public bool FlagToLoadSessionData = false;
    private bool isResettingData = false; // Flag to prevent auto-save during reset

    private void Awake()
    {
        // Only one SessionData ever exists — survives all scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerProgress = FindObjectOfType<PlayerProgress>();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !isResettingData && !NewGame)
        {
            SaveCurrentState();
        }
    }

    void OnApplicationQuit()
    {
        if (!isResettingData && !NewGame && FlagToLoadSessionData == true)
        {
            SaveCurrentState();
        }
    }

    // Call this before resetting data
    public void BeginDataReset()
    {
        isResettingData = true;
    }

    // Call this after reset is complete (optional)
    public void EndDataReset()
    {
        isResettingData = false;
        Debug.Log("Data reset complete - auto-save re-enabled");
    }

    private void SaveCurrentState()
    {
        SaveData.SaveAllGameData(
            trustBar.value, stressBar.value,
            TimerReference.elapsedTime, TimerReference.DayAdder, dayText.text,
            GameEngineReference.MissionTime1, GameEngineReference.MissionTime2,
            GameEngineReference.TimeToTriggerEvent1, GameEngineReference.TimeToTriggerEvent2,
            SessionData.Instance.Mission1Entered, SessionData.Instance.Mission2Entered,
            SessionData.Instance.Event1Triggered, SessionData.Instance.Event2Triggered,
            SessionData.Instance.PlayerPosition, SessionData.Instance.IsMuted
        );
    }

    // Called by PressDoor before leaving Scene 1
    public void SaveScene1State(float trust, float stress, Vector3 playerPos, float elapsedTime, int dayAdder, 
    string daysText, int missionTime1, int missionTime2, int timeToTriggerEvent1, int timeToTriggerEvent2, bool mission1Entered, bool mission2Entered, bool event1Triggered, bool event2Triggered, bool flagToSaveSessionData)
    {
        Trust = trust;
        Stress = stress;
        PlayerPosition = playerPos;
        ElapsedTime = elapsedTime;
        DayAdder = dayAdder;
        DaysText = daysText;
        MissionTime1 = missionTime1;
        MissionTime2 = missionTime2;
        TimeToTriggerEvent1 = timeToTriggerEvent1;
        TimeToTriggerEvent2 = timeToTriggerEvent2;
        Mission1Entered = mission1Entered;
        Mission2Entered = mission2Entered;
        Event1Triggered = event1Triggered;
        Event2Triggered = event2Triggered;
        FlagToLoadSessionData = flagToSaveSessionData;
    }

    // Called by EventManager after every choice
    public void UpdateBars(float trust, float stress)
    {
        Trust = trust;
        Stress = stress;
    }

    // Called by Pause when mute is toggled
    public void SetMuteState(bool muted)
    {
        IsMuted = muted;
    }
}