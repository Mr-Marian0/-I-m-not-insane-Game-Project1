using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float StressData;
    public float TrustData;

    public PlayerData(PlayerProgress player_progress)
    {
        StressData = player_progress.StressBar;
        TrustData = player_progress.TrustBar;
    }

}
