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
        if (SaveData.HasSaveFile() && SaveData.HasSaveFile())
        {
            startFunctions.SetButtonText(mainButton, "CONTINUE");
        }
        else
        {
            startFunctions.SetButtonText(mainButton, "NEW GAME");
        }
    }
}
