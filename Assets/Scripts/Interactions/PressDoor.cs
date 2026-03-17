using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressDoor : MonoBehaviour
{

    public GameEngine game_engine;
    public Animator Anim;

    public Transform PlayerPosition;

    public GameEngine GetGeneratedChallenge;

    public void DoorPressed()
    {
        Debug.Log("CHALLENGE: " + GetGeneratedChallenge.GenerateChallenge);

        Anim.SetBool("EnterExitDoor", true);
        StartCoroutine(Delay());

        //Turn Off UI Objects, During a Puzzle
        game_engine.Clock.SetActive(false);
        game_engine.Days.SetActive(false);
        game_engine.AmPm.SetActive(false);
        game_engine.JoyStick.SetActive(false);

        game_engine.UI.sortingOrder = 7;

        if(GetGeneratedChallenge.GenerateChallenge == 1) LoadScene("Riddles");
        else if(GetGeneratedChallenge.GenerateChallenge == 2) LoadScene("JigsawPuzz");
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        Anim.SetBool("EnterExitDoor", false);
        game_engine.Player.SetActive(false);
        game_engine.Background.SetActive(false);
        //Transform Player or Reset
        PlayerPosition.transform.position = new Vector3(-0.05f, -2.91f, 0);
    }

}
