using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float StressData;
    public float TrustData;
    public float[] position;

    public float ElapsedTime;
    public int DayAdder;
    public string DaysText;

    public int MissionTime1;
    public int MissionTime2;
    public int TimeToTriggerEvent1;
    public int TimeToTriggerEvent2;

    public float[] PlayerPosition;
    public bool IsMuted;
    public bool Mission1Entered;
    public bool Mission2Entered;
    public bool Event1Triggered;
    public bool Event2Triggered;

    public PlayerData() { }  // Empty constructor - fields will be set manually

}
