using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PressDoor : MonoBehaviour
{
    public GameEngine game_engine;
    public Animator Anim;

    public Transform PlayerPosition;

    public GameEngine GetGeneratedChallenge;

    // Reference to Timer so we can save its state
    public Timer SceneTimer;

    // Reference to the stress and trust sliders via EventManager
    public EventManager eventManager;

    public void DoorPressed()
    {
        // Debug.Log("CHALLENGE: " + GetGeneratedChallenge.GenerateChallenge);

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
                "DAY " + SceneTimer.DayAdder
            );

            
        }

        Anim.SetBool("EnterExitDoor", true);
        StartCoroutine(Delay());

        // Turn Off UI Objects during a Puzzle
        game_engine.Clock.SetActive(false);
        game_engine.Days.SetActive(false);
        game_engine.AmPm.SetActive(false);
        game_engine.JoyStick.SetActive(false);

        game_engine.UI.sortingOrder = 7;

        if (GetGeneratedChallenge.GenerateChallenge == 1) LoadScene("Riddles");
        else if (GetGeneratedChallenge.GenerateChallenge == 2) LoadScene("JigsawPuzz");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        Anim.SetBool("EnterExitDoor", false);
        game_engine.Player.SetActive(false);
        game_engine.Background.SetActive(false);
        // Transform Player or Reset
        PlayerPosition.transform.position = new Vector3(-0.05f, -2.91f, 0);
    }

    
}