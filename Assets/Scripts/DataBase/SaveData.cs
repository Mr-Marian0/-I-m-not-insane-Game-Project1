using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static void SavePlayer(float trust, float stress, PlayerProgress player_progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.bimbo";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player_progress);
        data.TrustData = trust;
        data.StressData = stress;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayer(float trust, float stress)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.bimbo";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(trust, stress);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.bimbo";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }

}
