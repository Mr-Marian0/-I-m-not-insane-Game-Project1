using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PressDoor : MonoBehaviour
{
    public GameEngine game_engine;
    public Transform PlayerPosition;

    public GameEngine GameEngineReference;

    // Reference to Timer so we can save its state
    public Timer SceneTimer;

    // Reference to the stress and trust sliders via EventManager
    public EventManager eventManager;

    public void DoorPressed()
    {
        GameEngineReference.flagMissionDoor = true;
        
        // Determine which mission is being entered and mark it as entered
        bool mission1Entered = SessionData.Instance != null ? SessionData.Instance.Mission1Entered : false;
        bool mission2Entered = SessionData.Instance != null ? SessionData.Instance.Mission2Entered : false;
        bool event1Triggered = SessionData.Instance != null ? SessionData.Instance.Event1Triggered : false;
        bool event2Triggered = SessionData.Instance != null ? SessionData.Instance.Event2Triggered : false;
        
        if (SceneTimer != null)
        {
            if (SceneTimer.minutes >= GameEngineReference.MissionTime1 && SceneTimer.minutes < GameEngineReference.MissionTime1 + 2)
            {
                mission1Entered = true;
            }
            else if (SceneTimer.minutes >= GameEngineReference.MissionTime2 && SceneTimer.minutes < GameEngineReference.MissionTime2 + 2)
            {
                mission2Entered = true;
            }
        }
        
        // Save everything into SessionData before leaving Scene 1
        if (SessionData.Instance != null && SceneTimer != null && eventManager != null)
        {
            Debug.Log("Elapsed TIME SAVED: "+SceneTimer.elapsedTime+" | DayAdder SAVED: "+SceneTimer.DayAdder);
            SessionData.Instance.SaveScene1State(
                eventManager.trustBar.value,
                eventManager.stressBar.value,
                PlayerPosition.position,
                SceneTimer.elapsedTime,
                SceneTimer.DayAdder,
                "DAY " + SceneTimer.DayAdder,
                GameEngineReference.MissionTime1,
                GameEngineReference.MissionTime2,
                GameEngineReference.TimeToTriggerEvent1,
                GameEngineReference.TimeToTriggerEvent2,
                mission1Entered,
                mission2Entered,
                event1Triggered,
                event2Triggered
            );

            
        }
        

        // Turn Off UI Objects during a Puzzle
        game_engine.Clock.SetActive(false);
        game_engine.Days.SetActive(false);
        game_engine.JoyStick.SetActive(false);

        game_engine.UI.sortingOrder = 7;

        if (GameEngineReference.GenerateChallenge == 1) LoadScene("Riddles");
        else if (GameEngineReference.GenerateChallenge == 2) LoadScene("JigsawPuzz");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        game_engine.Player.SetActive(false);
        game_engine.Background.SetActive(false);
        // Transform Player or Reset
        PlayerPosition.transform.position = new Vector3(-0.05f, -2.91f, 0);
    }
}