using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressToSleep : MonoBehaviour
{

    bool delayStarter = false;
    public Animator Anim;
    [SerializeField] Timer timerReference;


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
    Debug.Log("Fast forward...");

    if (delayStarter == true)
    {
        Anim.SetBool("EnterExit", false);
        Anim.SetBool("WakeUp", false);

        // ✅ Reset timer speed back to normal
        timerReference.waitMin = 1f;
        timerReference.waitMax = 3f;

        Debug.Log("Sleep Canceled");
        delayStarter = false;
    }
    else
    {
        // ✅ Speed up timer when sleeping
        timerReference.waitMin = 0.5f;
        timerReference.waitMax = 1f;
    }
}

}