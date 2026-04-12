using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public Button mainButton; // The button that will be "CONTINUE" or "NEW GAME"
    public StartFunctions startFunctions; // Reference to StartFunctions script

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        PlayerData data = SaveData.LoadPlayer();
        if (data != null && (data.TrustData > 0 || data.StressData > 0))
        {
            startFunctions.SetButtonText(mainButton, "CONTINUE");
        }
        else
        {
            startFunctions.SetButtonText(mainButton, "NEW GAME");
        }
    }
}
