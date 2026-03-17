using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressToSleep : MonoBehaviour
{

    public Timer FastForwardTime;
    bool delayStarter = false;
    public Animator Anim;

    void Start()
    {
    }
    void Update()
    {
        
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);
        Anim.SetBool("WakeUp", true);
        delayStarter = true;
        Debug.Log("Press any key to cancel...");
    }

    public void SleepButtonPressed()
    {

        Anim.SetBool("EnterExit", true);

        StartCoroutine(StartDelay());

        FastForwardTime.FastForward = 4;

        Debug.Log("Fast foward...");

        if (delayStarter == true)
        {
            Anim.SetBool("EnterExit", false);
            Anim.SetBool("WakeUp", false);

            Debug.Log("Sleep Canceled");
            delayStarter = false;
            FastForwardTime.FastForward = 1;
        }
        
    }

}