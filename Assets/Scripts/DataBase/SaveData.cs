using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    private static string SavePath => Application.persistentDataPath + "/player.bimbo";

    public static void SavePlayer(float trust, float stress, PlayerProgress player_progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(SavePath, FileMode.Create);

        PlayerData data = new PlayerData(player_progress);
        data.TrustData = trust;
        data.StressData = stress;

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