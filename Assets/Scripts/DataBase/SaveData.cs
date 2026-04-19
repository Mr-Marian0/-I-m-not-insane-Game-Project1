using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    private static string SavePath => Application.persistentDataPath + "/player.bimbo";

    public static void SaveAllGameData(
    float trust, float stress,
    float elapsedTime, int dayAdder, string daysText,
    int missionTime1, int missionTime2, 
    int eventTime1, int eventTime2,
    bool mission1Entered, bool mission2Entered,
    bool event1Triggered, bool event2Triggered,
    Vector3 playerPosition, bool isMuted)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(SavePath, FileMode.Create);
        
        PlayerData data = new PlayerData();
        
        // Set all values from parameters
        data.TrustData = trust;
        data.StressData = stress;
        data.ElapsedTime = elapsedTime;
        data.DayAdder = dayAdder;
        data.DaysText = daysText;
        data.MissionTime1 = missionTime1;
        data.MissionTime2 = missionTime2;
        data.TimeToTriggerEvent1 = eventTime1;
        data.TimeToTriggerEvent2 = eventTime2;
        data.Mission1Entered = mission1Entered;
        data.Mission2Entered = mission2Entered;
        data.Event1Triggered = event1Triggered;
        data.Event2Triggered = event2Triggered;
        data.PlayerPosition = new float[3] { playerPosition.x, playerPosition.y, playerPosition.z };
        data.IsMuted = isMuted;
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    //This FUNCTION REPLACES THE OLD SAVED FILES.
    public static void SavePlayer(float trust, float stress)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(SavePath, FileMode.Create);

        PlayerData data = new PlayerData(trust, stress);

        if (SessionData.Instance != null)
        {
            data.ElapsedTime = SessionData.Instance.ElapsedTime;
            data.DayAdder = SessionData.Instance.DayAdder;
            data.DaysText = SessionData.Instance.DaysText;
            data.MissionTime1 = SessionData.Instance.MissionTime1;
            data.MissionTime2 = SessionData.Instance.MissionTime2;
            data.TimeToTriggerEvent1 = SessionData.Instance.TimeToTriggerEvent1;
            data.TimeToTriggerEvent2 = SessionData.Instance.TimeToTriggerEvent2;
            data.PlayerPosition = new float[3] { SessionData.Instance.PlayerPosition.x, SessionData.Instance.PlayerPosition.y, SessionData.Instance.PlayerPosition.z };
            data.IsMuted = SessionData.Instance.IsMuted;
            data.Mission1Entered = SessionData.Instance.Mission1Entered;
            data.Mission2Entered = SessionData.Instance.Mission2Entered;
            data.Event1Triggered = SessionData.Instance.Event1Triggered;
            data.Event2Triggered = SessionData.Instance.Event2Triggered;
        }

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(SavePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(SavePath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + SavePath);
            return null;
        }
    }

    public static bool HasSaveFile()
    {
        return File.Exists(SavePath) && new FileInfo(SavePath).Length > 0;
    }

    public static void DeleteSave()
    {
        // Tell SessionData not to auto-save during deletion
        if (SessionData.Instance != null)
        {
            SessionData.Instance.BeginDataReset();
        }
        
        try
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log("Save file deleted successfully");
            }
            else
            {
                Debug.Log("No save file to delete");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to delete save: {e.Message}");
        }
    }
}