using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressToSleep : MonoBehaviour
{
    public GameObject SleepButton;
    [SerializeField] Timer timerReference;

    [Header("Stress")]
    public Slider stressSlider;

    [Header("Warning Text")]
    public GameObject BlackOutReference;
    public TextMeshProUGUI warningText;

    [Header("Animator")]
    public Animator Anim;

    private Vector3 buttonOriginalPos;

    void Start()
    {

        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    // 🔥 Button OnClick
    public void SleepButtonPressed()
    {
        // 🔴 If currently sleeping → WAKE UP
        if (IsSleeping)
        {
            WakeUP();

            IsSleeping = false;
            return;
        }

        // 🔴 If stress too high → BLOCK
        if (stressSlider.value >= 40f)
        {
            StopAllCoroutines();
            StartCoroutine(StressWarning());
            return;
        }

        // 🟢 Otherwise → go to sleep
        StartCoroutine(StartDelay());
    }

    // ✅ YOUR ORIGINAL CODE (UNCHANGED)
    IEnumerator StartDelay()
    {
        isGoingToBed = true;

        Anim.SetBool("EnterExit", true);

        yield return new WaitForSeconds(0.5f);

        Anim.SetBool("WakeUp", true);

        // 🔥 Restore normal time
        timerReference.waitMin = 0.5f;
        timerReference.waitMax = 1f;

        IsSleeping = true;
        isGoingToBed = false;
    }
    public void WakeUP()
    {
        Anim.SetBool("WakeUp", false);
        Anim.SetBool("EnterExit", false);

        // 🔥 Restore normal time
        timerReference.waitMin = 1f;
        timerReference.waitMax = 3f;
    }
    // ⚠️ ONLY runs when stress is too high
    IEnumerator StressWarning()
    {
        // activate text (I set it inactive in inspector)
        BlackOutReference.SetActive(true);
        warningText.gameObject.SetActive(true);

        Color c = warningText.color;
        c.a = 1f;
        warningText.color = c;

        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float offset = Mathf.Sin(time * 25f) * 15f;

            // shake SleepButton ONLY
            SleepButton.transform.localPosition =
                buttonOriginalPos + new Vector3(offset, 0, 0);

            yield return null;
        }

        // reset button position
        SleepButton.transform.localPosition = buttonOriginalPos;

        // fade out text
        float fadeTime = 0f;
        while (fadeTime < 0.5f)
        {
            fadeTime += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, fadeTime / 0.5f);
            Color newC = warningText.color;
            newC.a = alpha;
            warningText.color = newC;

            yield return null;
        }

        warningText.gameObject.SetActive(false);
    }

    // 🔴 Make sure these exist (since you used them)
    private bool isGoingToBed;
    public bool IsSleeping;
}