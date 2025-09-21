using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerMission;

    public float TimeLimit;
    public int minutes;
    public int seconds;

    public bool PauseTheTimer = true;
   
    void Update()
    {

        //PAUSE
        if(PauseTheTimer)
        {
            minutes = Mathf.FloorToInt(TimeLimit / 60);
            seconds = Mathf.FloorToInt(TimeLimit % 60);
            TimerMission.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        //RESET


        //TIMER DURING CHALLENGE
        if (TimeLimit > 0)
        {
            TimeLimit -= Time.deltaTime;
        }
        // DETECT IF THE TIMER ENDS
        else if (TimeLimit < 0)
        {
            TimeLimit = 0;
            TimerMission.color = Color.red;
        }
        
    }

    public void OnEnable()
    {
        TimeLimit = 60;
    }

    public void OnDisable(){
        TimerMission.color = Color.white;
    }

}
