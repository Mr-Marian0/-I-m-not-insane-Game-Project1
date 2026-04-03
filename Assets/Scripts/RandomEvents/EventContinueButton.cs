using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventContinueButton : MonoBehaviour
{

    public GameObject EventStarterCanvas;
    public GameEngine gameEngineReference;

     public void Continue()
    {
        gameEngineReference.ResetFlagEventTriggered();
        EventStarterCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void EndTheEvent()
    {
        Time.timeScale = 1;
        EventStarterCanvas.SetActive(false);
    }
}
