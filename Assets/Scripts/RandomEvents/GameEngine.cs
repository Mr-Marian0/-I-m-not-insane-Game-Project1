using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public Canvas UI;

    // UI References
    public GameObject Stress;
    public GameObject Trust;
    public GameObject Clock;
    public GameObject Days;
    public GameObject AmPm;
    public GameObject JoyStick;
    public GameObject Player;
    public GameObject Background;
    public GameObject UI_Object;
    public Timer timer;
    public EventManager eventManager;
    public GameObject EnableDoor;

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

    void Start()
    {
        Time.timeScale = 1;
        GenerateNewTimes();// Generate all times at start
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

        // Reset everything at the end of the day (minute 24)
        if (timer.minutes == 24)
        {
            GenerateNewTimes();
            flagEventAndMissionTriggered = false;
            hasTriggeredSecondEvent = false;
            EnableDoor.SetActive(false);
            Debug.Log("New day started - New missions and events generated");
        }

        // ==================== MISSION LOGIC ====================

        // First Mission - Active for 2 minutes
        if (timer.minutes == MissionTime1 || 
            (timer.minutes > MissionTime1 && timer.minutes < MissionTime1 + 2))
        {
            EnableDoor.SetActive(true);
        }

        // Second Mission - Active for 2 minutes
        else if (timer.minutes == MissionTime2 || 
                (timer.minutes > MissionTime2 && timer.minutes < MissionTime2 + 2))
        {
            EnableDoor.SetActive(true);
        }
        else
        {
            EnableDoor.SetActive(false);
        }

        // ==================== EVENT LOGIC ====================

        // First Event
        if (!flagEventAndMissionTriggered && timer.minutes == TimeToTriggerEvent1)
        {
            flagEventAndMissionTriggered = true;
            eventManager.TriggerRandomEvent();
            Debug.Log("First Event Triggered at minute: " + TimeToTriggerEvent1);
            Time.timeScale = 0;
        }

        // Second Event
        if (!hasTriggeredSecondEvent && timer.minutes == TimeToTriggerEvent2)
        {
            hasTriggeredSecondEvent = true;
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