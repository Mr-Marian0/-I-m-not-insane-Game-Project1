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

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayer(float trust, float stress)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(SavePath, FileMode.Create);

        PlayerData data = new PlayerData(trust, stress);

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
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }
}
