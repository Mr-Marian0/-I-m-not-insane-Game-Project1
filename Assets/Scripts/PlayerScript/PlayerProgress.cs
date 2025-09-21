using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{

    public Slider StressSlider;
    public Slider TrustSlider;

    public float StressBar;
    public float TrustBar;

    public void Update()
    {
        StressBar = StressSlider.maxValue;
        TrustBar = TrustSlider.maxValue;
    }  

    public void SavePlayer()
    {
        SaveData.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayer();

        StressBar = data.StressData;
        TrustBar = data.TrustData;

    }

}
