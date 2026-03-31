using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public Canvas UI;

    //UI to Enable and Disable
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
    public int GenerateMissionStart;
    public int GenerateChallenge;
    public GameObject EnableDoor;

    void Start()
    {

        Time.timeScale = 1; //In case the game is paused, it will unpause when you start the game

        GenerateMissionStart = Random.Range(3, 11);
        Debug.Log("MISSION: " + GenerateMissionStart);

        GenerateChallenge = Random.Range(1, 3);
    }
    void Update()
    {

        //Show UI after the INTRO
       if(Mathf.FloorToInt(Time.time) == 39){
         Player.SetActive(true);
         UI_Object.SetActive(true);
        }

        //Used to Restart Missions/Challenge
        if(timer.minutes == 12)
        {
            GenerateMissionStart = Random.Range(3, 11);
            Debug.Log("MISSION: "+GenerateMissionStart);
        }

        //timer inherit to start the door open
        if(timer.minutes == GenerateMissionStart)
        {
            EnableDoor.SetActive(true);
        }
        // close the door again
        if(timer.minutes == 12)
        {
            EnableDoor.SetActive(false);
        }
    }
}
