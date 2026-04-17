using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject CheckCongratulation;

    float yScale = 0;
    bool Signal1 = false;
    public RectTransform Back2;
    public GameObject Show_Pause_Menu;
    public GameObject Mute;
    public GameObject Restart;
    public GameObject Surrender;
    public GameObject ResetData;
    public GameObject Exit;
    public GameObject Pause_Button;
    public Image Pause_Button_Image;
    public GameObject YouLose;

    // Restart functions to update the sliders
    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressTextPoints;
    public RectTransform MoveTrustPosition;
    public Vector3 TrustDefaultPosXY;
    public RectTransform MoveStressPosition;
    public Vector3 StressDefaultPosXY;
    public Slider TrustReward;
    public Slider StressReward;

    private bool isSoundMuted = false;
    private Image MuteButtonImage;
    public Sprite SoundOnSprite;
    public Sprite SoundOffSprite;
    public TextMeshProUGUI SoundButton_text;
    private PlayerProgress playerProgress;
    public Timer timerReference;

    public void PausePressed()
    {
        Pause_Button_Image.enabled = false;
        Time.timeScale = 0;
        Show_Pause_Menu.SetActive(true);
        Signal1 = true;


        StartCoroutine(ActivateDelay(0.5f));
    }

    public void Continue()
    {
        Pause_Button_Image.enabled = true;
        Time.timeScale = 1;
        Pause_Button.SetActive(true);
        Show_Pause_Menu.SetActive(false);
        Mute.SetActive(false);

        if (ResetData) ResetData.SetActive(false);
        if (Restart) Restart.SetActive(false);
        if (Surrender) Surrender.SetActive(false);

        Exit.SetActive(false);
        StopAllCoroutines();
    }

    public void ToggleSound()
    {
        isSoundMuted = !isSoundMuted;
        AudioListener.pause = isSoundMuted;
        MuteButtonImage.sprite = isSoundMuted ? SoundOffSprite : SoundOnSprite;
        SoundButton_text.text = isSoundMuted ? "Sound OFF" : "Sound ON";

        // Save mute state to SessionData so it survives scene transitions
        if (SessionData.Instance != null)
        {
            SessionData.Instance.SetMuteState(isSoundMuted);
        }
    }

    public void RestartPressed()
    {
        SceneManager.LoadScene("JigsawPuzz");
    }

    public void ResetDataPressed()
    {
        if (playerProgress != null)
        {
            playerProgress.StressSlider.value = 0f;
            playerProgress.TrustSlider.value = 0f;
            playerProgress.StressBar = 0f;
            playerProgress.TrustBar = 0f;
            timerReference.elapsedTime = 0f;
            timerReference.DayAdder = 1;
        }

        SessionData.Instance.NewGame = true;
        SaveData.DeleteSave();

        // Also reset SessionData bars
        if (SessionData.Instance != null)
        {
            SessionData.Instance.UpdateBars(0f, 0f);
            SessionData.Instance.ElapsedTime = 0f;
            SessionData.Instance.DayAdder = 1;
            SessionData.Instance.DaysText = "DAY 1";
        }

        SceneManager.LoadScene("StartMenu");
    }

    public void SurrenderPressed()
    {
        Pause_Button.SetActive(false);
        Show_Pause_Menu.SetActive(false);
        YouLose.SetActive(true);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);

        Time.timeScale = 0;

        MoveTrustPosition.anchoredPosition = new Vector2(-14.9f, 83.8f);
        MoveStressPosition.anchoredPosition = new Vector2(-14.9f, -67.94698f);

        CheckCongratulation.SetActive(true);

        TrustReward.value += 0;
        TrustTextPoints.text = "+0";
        TrustTextPoints.color = Color.gray;

        StressReward.value += 20;
        StressTextPoints.text = "+20";

        SaveData.SavePlayer(TrustReward.value, StressReward.value);

        // Sync surrender result into SessionData
        if (SessionData.Instance != null)
        {
            SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
        }
    }

    public void ExitPressed()
    {
        // Save data when exit is pressed
        SaveGameState();

        Time.timeScale = 1;
        Application.Quit();
        Debug.Log("Exit");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void SaveGameState()
    {
        // Get actual timer values from Timer.cs
        if (timerReference == null)
            timerReference = FindObjectOfType<Timer>();

        if (timerReference != null && SessionData.Instance != null)
        {
            // Sync Timer values into SessionData before saving
            SessionData.Instance.ElapsedTime = timerReference.elapsedTime;
            SessionData.Instance.DayAdder = timerReference.DayAdder;
            SessionData.Instance.DaysText = "DAY " + timerReference.DayAdder;
        }

        // Now save with the synced data
        if (playerProgress != null)
        {
            playerProgress.SavePlayer(playerProgress.TrustBar, playerProgress.StressBar);
        }
        else
        {
            SaveData.SavePlayer(0f, 0f);
        }

        Debug.Log("Game state saved with Timer values - Time: " + (timerReference != null ? timerReference.elapsedTime : 0) + 
                  ", Day: " + (timerReference != null ? timerReference.DayAdder : 1));
    }

    void Start()
    {
        Pause_Button_Image = Pause_Button.GetComponent<Image>();
        playerProgress = FindObjectOfType<PlayerProgress>();

        if (Mute != null)
        {
            MuteButtonImage = Mute.GetComponent<Image>();
        }

        if (MuteButtonImage != null && MuteButtonImage.sprite != null)
        {
            SoundOnSprite = MuteButtonImage.sprite;
        }

        // Restore mute state from SessionData when any scene loads
        if (SessionData.Instance != null)
        {
            isSoundMuted = SessionData.Instance.IsMuted;
            AudioListener.pause = isSoundMuted;

            if (MuteButtonImage != null)
                MuteButtonImage.sprite = isSoundMuted ? SoundOffSprite : SoundOnSprite;

            if (SoundButton_text != null)
                SoundButton_text.text = isSoundMuted ? "Sound OFF" : "Sound ON";
        }
    }

    void Update()
    {
        if (CheckCongratulation != null && CheckCongratulation.activeSelf)
        {
            Pause_Button.SetActive(false);
        }
        else
        {
            Pause_Button.SetActive(true);
        }

        if (Signal1 == true)
        {
            if (yScale >= 1)
            {
                yScale = 0;
                Signal1 = false;
            }
            else
            {
                yScale += Time.unscaledDeltaTime * 2;
                Back2.localScale = new Vector3(Back2.localScale.x, yScale, Back2.localScale.z);
            }
        }
    }

    IEnumerator ActivateDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Mute.SetActive(true);

        if (ResetData) ResetData.SetActive(true);
        if (Restart) Restart.SetActive(true);
        if (Exit) Exit.SetActive(true);
        if (Surrender) Surrender.SetActive(true);
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        if (TrustTextPoints != null) TrustTextPoints.color = Color.green;
    }
}