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

    public PlayerData(float elapsedTime, int dayAdder, string daysText, int missionTime1, int missionTime2, int timeToTriggerEvent1, int timeToTriggerEvent2)
    {
        ElapsedTime = elapsedTime;
        DayAdder = dayAdder;
        DaysText = daysText;
        MissionTime1 = missionTime1;
        MissionTime2 = missionTime2;
        TimeToTriggerEvent1 = timeToTriggerEvent1;
        TimeToTriggerEvent2 = timeToTriggerEvent2;
    }

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
            PlayerPosition = new float[3];
            PlayerPosition[0] = SessionData.Instance.PlayerPosition.x;
            PlayerPosition[1] = SessionData.Instance.PlayerPosition.y;
            PlayerPosition[2] = SessionData.Instance.PlayerPosition.z;
            IsMuted = SessionData.Instance.IsMuted;
            Mission1Entered = SessionData.Instance.Mission1Entered;
            Mission2Entered = SessionData.Instance.Mission2Entered;
            Event1Triggered = SessionData.Instance.Event1Triggered;
            Event2Triggered = SessionData.Instance.Event2Triggered;
        }
        else
        {
            ElapsedTime = 0f;
            DayAdder = 1;
            DaysText = "DAY 1";
            PlayerPosition = new float[3];
            IsMuted = false;
            Mission1Entered = false;
            Mission2Entered = false;
            Event1Triggered = false;
            Event2Triggered = false;
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
        PlayerPosition = new float[3];
        IsMuted = false;
        Mission1Entered = false;
        Mission2Entered = false;
        Event1Triggered = false;
        Event2Triggered = false;
    }

}
