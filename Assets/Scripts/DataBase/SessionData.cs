using UnityEngine;

public class SessionData : MonoBehaviour
{
    public static SessionData Instance;

    [Header("Bar Values")]
    public float Trust = 50f;
    public float Stress = 50f;

    [Header("Player Position")]
    public Vector3 PlayerPosition = new Vector3(-0.05f, -2.91f, 0);

    [Header("Timer")]
    public float ElapsedTime = 0f;
    public int DayAdder = 1;
    public string DaysText = "DAY 1";

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

    private PlayerProgress playerProgress;

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
        if (pauseStatus)
        {
            SaveCurrentState();
        }
    }

    void OnApplicationQuit()
    {
        SaveCurrentState();
    }

    private void SaveCurrentState()
    {
        if (playerProgress == null)
            playerProgress = FindObjectOfType<PlayerProgress>();

        if (playerProgress != null)
        {
            SaveData.SavePlayer(Trust, Stress, playerProgress);
        }
        else
        {
            SaveData.SavePlayer(Trust, Stress);
        }

        Debug.Log("SessionData auto-saved on pause/quit");
    }

    // Called by PressDoor before leaving Scene 1
    public void SaveScene1State(float trust, float stress, Vector3 playerPos, float elapsedTime, int dayAdder, 
    string daysText, int missionTime1, int missionTime2, int timeToTriggerEvent1, int timeToTriggerEvent2, bool mission1Entered, bool mission2Entered, bool event1Triggered, bool event2Triggered)
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