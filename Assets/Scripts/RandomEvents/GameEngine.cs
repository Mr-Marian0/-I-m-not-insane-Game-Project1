using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public Canvas UI;

    // UI References
    public GameObject Stress;
    public GameObject Trust;
    public GameObject Clock;
    public GameObject Days;
    public GameObject JoyStick;
    public GameObject Player;
    public GameObject Background;
    public GameObject UI_Object;
    public Timer timer;
    public EventManager eventManager;
    public GameObject EnableDoor;
    public PressToSleep pressToSleepReference;

    // Mission Times
    public int MissionTime1;      // First mission (3-11)
    public int MissionTime2;      // Second mission (13-22)

    // Event Times
    public int TimeToTriggerEvent1;   // First event (3-11)
    public int TimeToTriggerEvent2;   // Second event (13-22)

    public int GenerateChallenge;

    // Flags
    bool flagEventAndMissionTriggered = false;
    bool hasTriggeredSecondEvent = false;
    bool event1Triggered = false;
    bool event2Triggered = false;
    bool hasGeneratedNewDay = false;
    public bool flagMissionDoor = false;
    bool mission1Activated = false;
    bool mission2Activated = false;

    void Start()
    {
        Time.timeScale = 1;

        if (timer.minutes == 0) GenerateNewTimes();
        // Restore generated MISSION and EVENT from SessionData if returning from a mission
        if (SessionData.Instance != null && SessionData.Instance.MissionTime1 != 0 && SessionData.Instance.MissionTime2 != 0)
        {
            MissionTime1 = SessionData.Instance.MissionTime1;
            MissionTime2 = SessionData.Instance.MissionTime2;
            TimeToTriggerEvent1 = SessionData.Instance.TimeToTriggerEvent1;
            TimeToTriggerEvent2 = SessionData.Instance.TimeToTriggerEvent2;
            event1Triggered = SessionData.Instance.Event1Triggered;
            event2Triggered = SessionData.Instance.Event2Triggered;
        }

        GenerateChallenge = Random.Range(1, 3);
    }

    void Update()
{
    // Show UI after intro
    if (Mathf.FloorToInt(Time.time) == 39)
    {
        Player.SetActive(true);
        UI_Object.SetActive(true);
    }

    if (timer.minutes < 24)
    {
        hasGeneratedNewDay = false;
    }

    if (timer.minutes == 24 && !hasGeneratedNewDay)
    {
        GenerateNewTimes();
        hasGeneratedNewDay = true;
        hasTriggeredSecondEvent = false;
        EnableDoor.SetActive(false);
        Debug.Log("New day started - New missions and events generated");
    }

    // ==================== MISSION LOGIC ====================

    // First Mission - Active for 2 minutes
    if ((timer.minutes == MissionTime1 || (timer.minutes > MissionTime1 && timer.minutes < MissionTime1 + 2)) && 
        !flagMissionDoor && (SessionData.Instance == null || !SessionData.Instance.Mission1Entered))
    {
        if (!mission1Activated)
        {
            mission1Activated = true;
            if (pressToSleepReference.isActiveAndEnabled) pressToSleepReference.SleepButtonPressed();
        }
        EnableDoor.SetActive(true);
    }
    else
    {
        mission1Activated = false;
    }
    // Second Mission - Active for 2 minutes
    if ((timer.minutes == MissionTime2 || (timer.minutes > MissionTime2 && timer.minutes < MissionTime2 + 2)) && 
             !flagMissionDoor && (SessionData.Instance == null || !SessionData.Instance.Mission2Entered))
    {
        if (!mission2Activated)
        {
            mission2Activated = true;
            if (pressToSleepReference.isActiveAndEnabled) pressToSleepReference.SleepButtonPressed();
        }
        EnableDoor.SetActive(true);
    }
    else
    {
        mission2Activated = false;
    }

    // ==================== EVENT LOGIC ====================

    // First Event
    if (!flagEventAndMissionTriggered && !event1Triggered && timer.minutes == TimeToTriggerEvent1)
    {
        flagEventAndMissionTriggered = true;
        event1Triggered = true;
        if (SessionData.Instance != null)
        {
            SessionData.Instance.Event1Triggered = true;
        }
        eventManager.TriggerRandomEvent();
        Debug.Log("First Event Triggered at minute: " + TimeToTriggerEvent1);
        Time.timeScale = 0;
    }

    // Second Event
    if (!hasTriggeredSecondEvent && !event2Triggered && timer.minutes == TimeToTriggerEvent2)
    {
        hasTriggeredSecondEvent = true;
        event2Triggered = true;
        if (SessionData.Instance != null)
        {
            SessionData.Instance.Event2Triggered = true;
        }
        eventManager.TriggerRandomEvent();
        Debug.Log("Second Event Triggered at minute: " + TimeToTriggerEvent2);
        Time.timeScale = 0;
    }
}

    // Generate new times for both missions and events
    private void GenerateNewTimes()
    {
        // Missions
        MissionTime1 = Random.Range(3, 12);     // 3 to 11
        MissionTime2 = Random.Range(15, 22);    // 15 to 22

        // Events
        TimeToTriggerEvent1 = Random.Range(3, 12);
        TimeToTriggerEvent2 = Random.Range(13, 23);

        // Reset mission entered flags and event trigger flags for new day
        if (SessionData.Instance != null)
        {
            SessionData.Instance.Mission1Entered = false;
            SessionData.Instance.Mission2Entered = false;
            SessionData.Instance.Event1Triggered = false;
            SessionData.Instance.Event2Triggered = false;
        }

        mission1Activated = false;
        mission2Activated = false;

        Debug.Log("=== New Day Generated ===");
        Debug.Log("Mission 1: " + MissionTime1);
        Debug.Log("Mission 2: " + MissionTime2);
        Debug.Log("Event 1  : " + TimeToTriggerEvent1);
        Debug.Log("Event 2  : " + TimeToTriggerEvent2);
    }

    public void ResetFlagEventTriggered()
    {
        flagEventAndMissionTriggered = false;
    }
}