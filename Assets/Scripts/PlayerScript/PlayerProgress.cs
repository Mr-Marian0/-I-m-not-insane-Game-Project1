using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{

    public Slider StressSlider;
    public Slider TrustSlider;
    // ASFDASFA 
    

    public float StressBar;
    public float TrustBar;

    public void Update()
    {
        StressBar = StressSlider.value;
        TrustBar = TrustSlider.value;
    }  

    public void SavePlayer(float trustValue, float stressValue)
    {   
        SaveData.SavePlayer(trustValue, stressValue, this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayer();
        
        if (data != null)
        {
            StressBar = data.StressData;
            TrustBar = data.TrustData;
            
            // Apply values to the sliders
            StressSlider.value = data.StressData;
            TrustSlider.value = data.TrustData;
            Debug.Log("Loaded saved values - Trust: " + data.TrustData + ", Stress: " + data.StressData);
        }
    }
    
    public void Start()
    {
        LoadPlayer();
    }

}
