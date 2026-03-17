using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnEnableScript : MonoBehaviour
{

    //USED TO ENABLE AND DISABLE TIMER
    public GameObject StartTimer;

    public void OnEnable()
    {
        StartTimer.SetActive(true);
    }

    public void OnDisable()
    {
            if (StartTimer != null)
            {
                StartTimer.SetActive(false);
            }
            else
            {

            }
    }

}
