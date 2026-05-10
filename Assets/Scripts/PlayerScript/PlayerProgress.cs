using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{

    public Slider StressSlider;
    public Slider TrustSlider;
    public TextMeshProUGUI StressPercentageText;
    public TextMeshProUGUI TrustPercentageText;
    
    public Timer TimerReference;

    public void Update()
    {

        StressPercentageText.text = Mathf.RoundToInt(StressSlider.value).ToString();
        TrustPercentageText.text = Mathf.RoundToInt(TrustSlider.value).ToString();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayer();

        if (data != null)
        {
            TimerReference.elapsedTime = data.ElapsedTime;
            TimerReference.DayAdder = data.DayAdder;
            TimerReference.Days.text = string.IsNullOrEmpty(data.DaysText) ? "DAY " + data.DayAdder : data.DaysText;

            // if (SessionData.Instance != null)
            // {
            //     SessionData.Instance.ElapsedTime = data.ElapsedTime;
            //     SessionData.Instance.DayAdder = data.DayAdder;
            //     SessionData.Instance.DaysText = string.IsNullOrEmpty(data.DaysText) ? "DAY " + data.DayAdder : data.DaysText;
            //     SessionData.Instance.MissionTime1 = data.MissionTime1;
            //     SessionData.Instance.MissionTime2 = data.MissionTime2;
            //     SessionData.Instance.TimeToTriggerEvent1 = data.TimeToTriggerEvent1;
            //     SessionData.Instance.TimeToTriggerEvent2 = data.TimeToTriggerEvent2;

            //     if (data.PlayerPosition != null && data.PlayerPosition.Length == 3)
            //         SessionData.Instance.PlayerPosition = new Vector3(data.PlayerPosition[0], data.PlayerPosition[1], data.PlayerPosition[2]);

            //     SessionData.Instance.IsMuted = data.IsMuted;
            //     SessionData.Instance.Mission1Entered = data.Mission1Entered;
            //     SessionData.Instance.Mission2Entered = data.Mission2Entered;
            //     SessionData.Instance.Event1Triggered = data.Event1Triggered;
            //     SessionData.Instance.Event2Triggered = data.Event2Triggered;
            // }

            // Debug.Log("Loaded saved values - Trust: " + data.TrustData + ", Stress: " + data.StressData + ", Time: " + data.ElapsedTime + ", Day: " + data.DayAdder);
        }
    }
    
    public void Start()
    {
        LoadPlayer();
    }

}
