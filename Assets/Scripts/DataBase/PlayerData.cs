using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float StressData;
    public float TrustData;
    public float[] position;

    public PlayerData(PlayerProgress player_progress)
    {
        StressData = player_progress.StressBar;
        TrustData = player_progress.TrustBar;

        position = new float[3];
        position[0] = player_progress.transform.position.x;
        position[1] = player_progress.transform.position.y;
        position[2] = player_progress.transform.position.z;
    }

    public PlayerData(float trust, float stress)
    {
        TrustData = trust;
        StressData = stress;
        position = new float[3];
    }

}
