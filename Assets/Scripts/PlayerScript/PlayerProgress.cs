using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{

    public Slider StressSlider;
    public Slider TrustSlider;
    // ASFDASFA 
    

    public float StressBar;
    public float TrustBar;

    public void Update()
    {
        StressBar = StressSlider.value;
        TrustBar = TrustSlider.value;
    }  

    public void SavePlayer(float trustValue, float stressValue)
    {   
        SaveData.SavePlayer(trustValue, stressValue, this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayer();
        
        if (data != null)
        {
            StressBar = data.StressData;
            TrustBar = data.TrustData;
            
            // Apply values to the sliders
            StressSlider.value = data.StressData;
            TrustSlider.value = data.TrustData;

            if (SessionData.Instance != null)
            {
                SessionData.Instance.ElapsedTime = data.ElapsedTime;
                SessionData.Instance.DayAdder = data.DayAdder;
                SessionData.Instance.DaysText = string.IsNullOrEmpty(data.DaysText) ? "DAY " + data.DayAdder : data.DaysText;
                SessionData.Instance.MissionTime1 = data.MissionTime1;
                SessionData.Instance.MissionTime2 = data.MissionTime2;
                SessionData.Instance.TimeToTriggerEvent1 = data.TimeToTriggerEvent1;
                SessionData.Instance.TimeToTriggerEvent2 = data.TimeToTriggerEvent2;
            }

            Debug.Log("Loaded saved values - Trust: " + data.TrustData + ", Stress: " + data.StressData + ", Time: " + data.ElapsedTime + ", Day: " + data.DayAdder);
        }
    }
    
    public void Start()
    {
        LoadPlayer();
    }

}
