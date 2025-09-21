using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI AmPm;
    [SerializeField] TextMeshProUGUI Days;
    public int DayAdder = 1;
    public int AM = 1;
    public int PM = 0;
    public int minutes;
    public int seconds;

    [SerializeField] public float elapsedTime;
    public int FastForward = 1;

    void Update()
    {
        elapsedTime += Time.deltaTime * FastForward;
        minutes = Mathf.FloorToInt(elapsedTime / 2);
        seconds = Mathf.FloorToInt(elapsedTime % 2);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(minutes == 12)
        {
            DayAdder += 1;
            elapsedTime = 0;
            Days.text = "DAY " + DayAdder;
        }

        //Change the AM and PM text in UI
        if(minutes == 12 & PM == 1)
        {
            PM = 0;
            AM += 1;
            AmPm.text = "AM";
            Debug.Log("' Set to AM '");
        }
        else if(minutes == 12 & AM == 1)
        {
            AM = 0;
            PM += 1;
            AmPm.text = "PM";
            Debug.Log("' Set to PM '");
        }

    }
}
