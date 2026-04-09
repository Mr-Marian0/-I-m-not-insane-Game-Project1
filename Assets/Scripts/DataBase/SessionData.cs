using UnityEngine;

public class SessionData : MonoBehaviour
{
    public static SessionData Instance;

    [Header("Bar Values")]
    public float Trust = 50f;
    public float Stress = 50f;

    [Header("Player Position")]
    public Vector3 PlayerPosition = new Vector3(-0.05f, -2.91f, 0);

    [Header("Timer")]
    public float ElapsedTime = 0f;
    public int DayAdder = 1;
    public string DaysText = "DAY 1";

    [Header("Audio")]
    public bool IsMuted = false;

    private void Awake()
    {
        // Only one SessionData ever exists — survives all scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called by PressDoor before leaving Scene 1
    public void SaveScene1State(float trust, float stress, Vector3 playerPos, float elapsedTime, int dayAdder, string daysText)
    {
        Trust = trust;
        Stress = stress;
        PlayerPosition = playerPos;
        ElapsedTime = elapsedTime;
        DayAdder = dayAdder;
        DaysText = daysText;
    }

    // Called by EventManager after every choice
    public void UpdateBars(float trust, float stress)
    {
        Trust = trust;
        Stress = stress;
    }

    // Called by Pause when mute is toggled
    public void SetMuteState(bool muted)
    {
        IsMuted = muted;
    }
}