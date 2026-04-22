using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

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
    public PlayableDirector directorReference;
    public GameObject IntroSceneReference;
    public Timer timer;
    public EventManager eventManager;
    public GameObject EnableDoor;
    public GameObject TutorialContentReference;
    public PressToSleep pressToSleepReference;
    public TutorialFader tutorialFader;

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

    void Awake()
    {

    if (SaveData.HasSaveFile())
    {
        // Save exists - keep disabled (already disabled in Inspector)
        Time.timeScale = 1;
        Debug.Log("Save found - Intro stays disabled");
        Player.SetActive(true);
        UI_Object.SetActive(true);
    }
    else
    {
        // NO save - ENABLE the intro
        timer.StopTimer = true; // Stop the timer until the intro finishes
        Debug.Log("New game - Enabling intro");
        if (IntroSceneReference != null)
            IntroSceneReference.SetActive(true);  // THIS enables it
        Player.SetActive(false);
        UI_Object.SetActive(false);
    }
    }

    void Start()
    {
        bool hasSessionTimes = SessionData.Instance != null &&
            (SessionData.Instance.MissionTime1 != 0 || SessionData.Instance.MissionTime2 != 0 ||
             SessionData.Instance.TimeToTriggerEvent1 != 0 || SessionData.Instance.TimeToTriggerEvent2 != 0);

        if (hasSessionTimes)
        {
            MissionTime1 = SessionData.Instance.MissionTime1;
            MissionTime2 = SessionData.Instance.MissionTime2;
            TimeToTriggerEvent1 = SessionData.Instance.TimeToTriggerEvent1;
            TimeToTriggerEvent2 = SessionData.Instance.TimeToTriggerEvent2;
            event1Triggered = SessionData.Instance.Event1Triggered;
            event2Triggered = SessionData.Instance.Event2Triggered;

            // Restore second event flag so it doesn't re-trigger on scene reload
            hasTriggeredSecondEvent = SessionData.Instance.Event2Triggered;
            flagEventAndMissionTriggered = SessionData.Instance.Event1Triggered;
        }
        else if (SaveData.HasSaveFile())
        {
            Debug.Log("Save file found - Loading mission and event times");
            
            PlayerData save = SaveData.LoadPlayer();

            Debug.Log($"MissionTime1: {save.MissionTime1}");
            Debug.Log($"MissionTime2: {save.MissionTime2}");
            Debug.Log($"TimeToTriggerEvent1: {save.TimeToTriggerEvent1}");
            Debug.Log($"TimeToTriggerEvent2: {save.TimeToTriggerEvent2}");

            if (save != null &&
                (save.MissionTime1 != 0 || save.MissionTime2 != 0 ||
                 save.TimeToTriggerEvent1 != 0 || save.TimeToTriggerEvent2 != 0))
            {

                MissionTime1 = save.MissionTime1;
                MissionTime2 = save.MissionTime2;
                TimeToTriggerEvent1 = save.TimeToTriggerEvent1;
                TimeToTriggerEvent2 = save.TimeToTriggerEvent2;

                // FIX: Restore event flags from save file so they don't re-trigger
                event1Triggered = save.Event1Triggered;
                event2Triggered = save.Event2Triggered;
                hasTriggeredSecondEvent = save.Event2Triggered;
                flagEventAndMissionTriggered = save.Event1Triggered;

                if (SessionData.Instance != null)
                {
                    SessionData.Instance.MissionTime1 = save.MissionTime1;
                    SessionData.Instance.MissionTime2 = save.MissionTime2;
                    SessionData.Instance.TimeToTriggerEvent1 = save.TimeToTriggerEvent1;
                    SessionData.Instance.TimeToTriggerEvent2 = save.TimeToTriggerEvent2;

                    // FIX: Sync event flags back to SessionData so future scene loads use them
                    SessionData.Instance.Event1Triggered = save.Event1Triggered;
                    SessionData.Instance.Event2Triggered = save.Event2Triggered;
                }
            }
        }
        else
        {
            GenerateNewTimes();
        }

        if (timer.minutes == 0 && !hasSessionTimes && !SaveData.HasSaveFile())
        {
            // ensure there is always a generated mission/event range on a fresh start
            GenerateNewTimes();
        }

        GenerateChallenge = Random.Range(1, 3);
    }

    void Update()
    {
        if (directorReference != null && directorReference.state != PlayState.Playing && IntroSceneReference != null)
    {
        Debug.Log("Intro timeline finished - Starting game");
        // Intro finished - show UI but keep timescale 0
        Player.SetActive(true);
        UI_Object.SetActive(true);
        
        // Get the Canvas Group on UI_Object and fade it in
        CanvasGroup uiCanvasGroup = UI_Object.GetComponent<CanvasGroup>();
        if (uiCanvasGroup != null)
        {
            // Fade in the main UI (using unscaled time)
            StartCoroutine(FadeInUI(uiCanvasGroup));
        }
        else
        {
            // If no Canvas Group, just enable the UI
            UI_Object.SetActive(true);
            // Start tutorial sequence immediately
            if (tutorialFader != null) tutorialFader.StartTutorialSequence();
        }
        
        Destroy(IntroSceneReference); // Remove intro scene
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

        // Calculate if missions are active
        bool mission1Active = (timer.minutes == MissionTime1 || (timer.minutes > MissionTime1 && timer.minutes < MissionTime1 + 2)) && 
                              !flagMissionDoor && (SessionData.Instance == null || !SessionData.Instance.Mission1Entered);
        bool mission2Active = (timer.minutes == MissionTime2 || (timer.minutes > MissionTime2 && timer.minutes < MissionTime2 + 2)) && 
                              !flagMissionDoor && (SessionData.Instance == null || !SessionData.Instance.Mission2Entered);

        // First Mission - Active for 2 minutes
        if (mission1Active)
        {
            if (!mission1Activated)
            {
                mission1Activated = true;
                if (pressToSleepReference.isActiveAndEnabled) pressToSleepReference.SleepButtonPressed();
            }
        }
        else
        {
            mission1Activated = false;
        }

        // Second Mission - Active for 2 minutes
        if (mission2Active)
        {
            if (!mission2Activated)
            {
                mission2Activated = true;
                if (pressToSleepReference.isActiveAndEnabled) pressToSleepReference.SleepButtonPressed();
            }
        }
        else
        {
            mission2Activated = false;
        }

        // Set door active if either mission is active
        EnableDoor.SetActive(mission1Active || mission2Active);

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
            if(UI_Object.activeInHierarchy) Time.timeScale = 0;
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
            if(UI_Object.activeInHierarchy) Time.timeScale = 0;
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

        // Reset local flags for new day
        event1Triggered = false;
        event2Triggered = false;
        flagEventAndMissionTriggered = false;
        mission1Activated = false;
        mission2Activated = false;

        Debug.Log("=== New Day Generated ===");
        Debug.Log("Mission 1: " + MissionTime1);
        Debug.Log("Mission 2: " + MissionTime2);
        Debug.Log("Event 1  : " + TimeToTriggerEvent1);
        Debug.Log("Event 2  : " + TimeToTriggerEvent2);
    }

    private IEnumerator FadeInUI(CanvasGroup uiCanvasGroup)
    {
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;
        
        // Initially hide UI
        uiCanvasGroup.alpha = 0f;
        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;
        
        // Fade in using unscaled delta time (works with Time.timeScale = 0)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            uiCanvasGroup.alpha = alpha;
            yield return null;
        }
        
        uiCanvasGroup.alpha = 1f;
        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;
        
        // Now start the tutorial sequence
        if (tutorialFader != null && SaveData.HasSaveFile() == false)
        {
            tutorialFader.StartTutorialSequence();
        }
        else
        {
            TutorialContentReference.SetActive(false);
        }
    }
    public void ResetFlagEventTriggered()
    {
        flagEventAndMissionTriggered = false;
    }
}