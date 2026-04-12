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

    public PlayerData(PlayerProgress player_progress)
    {
        StressData = player_progress.StressBar;
        TrustData = player_progress.TrustBar;

        position = new float[3];
        position[0] = player_progress.transform.position.x;
        position[1] = player_progress.transform.position.y;
        position[2] = player_progress.transform.position.z;

        if (SessionData.Instance != null)
        {
            ElapsedTime = SessionData.Instance.ElapsedTime;
            DayAdder = SessionData.Instance.DayAdder;
            DaysText = SessionData.Instance.DaysText;
            MissionTime1 = SessionData.Instance.MissionTime1;
            MissionTime2 = SessionData.Instance.MissionTime2;
            TimeToTriggerEvent1 = SessionData.Instance.TimeToTriggerEvent1;
            TimeToTriggerEvent2 = SessionData.Instance.TimeToTriggerEvent2;
        }
        else
        {
            ElapsedTime = 0f;
            DayAdder = 1;
            DaysText = "DAY 1";
            MissionTime1 = 0;
            MissionTime2 = 0;
            TimeToTriggerEvent1 = 0;
            TimeToTriggerEvent2 = 0;
        }
    }

    public PlayerData(float trust, float stress)
    {
        TrustData = trust;
        StressData = stress;
        position = new float[3];
        ElapsedTime = 0f;
        DayAdder = 1;
        DaysText = "DAY 1";
        MissionTime1 = 0;
        MissionTime2 = 0;
        TimeToTriggerEvent1 = 0;
        TimeToTriggerEvent2 = 0;
    }

}
