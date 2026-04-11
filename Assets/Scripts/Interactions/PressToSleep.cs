using System.Collections;
using UnityEngine;

public class PressToSleep : MonoBehaviour
{
    public Animator Anim;
    [SerializeField] Timer timerReference;

    public bool IsSleeping { get; private set; }
    private bool isGoingToBed;
    private Coroutine sleepCoroutine;

    public void SleepButtonPressed()
    {
        if (IsSleeping)
        {
            WakeUp();
            return;
        }

        if (isGoingToBed)
            return;

        sleepCoroutine = StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        isGoingToBed = true;
        Anim.SetBool("EnterExit", true);
        yield return new WaitForSeconds(0.5f);
        Anim.SetBool("WakeUp", true);
        IsSleeping = true;
        isGoingToBed = false;

        timerReference.waitMin = 0.5f;
        timerReference.waitMax = 1f;
    }

    public void WakeUp()
    {
        if (sleepCoroutine != null)
        {
            StopCoroutine(sleepCoroutine);
            sleepCoroutine = null;
        }

        isGoingToBed = false;
        IsSleeping = false;

        Anim.SetBool("EnterExit", false);
        Anim.SetBool("WakeUp", false);

        timerReference.waitMin = 1f;
        timerReference.waitMax = 3f;
    }
}
