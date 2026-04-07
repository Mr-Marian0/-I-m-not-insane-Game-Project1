using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

  public GameObject CheckCongratulation;
  

  public RectTransform Back2;
  float yScale = 0;
  bool Signal1 = false;
  public GameObject Show_Pause_Menu;
  public GameObject Mute;
  public GameObject Restart;
  public GameObject Surrender;
  public GameObject ResetData;
  public GameObject Exit;
  public GameObject Pause_Button;
  public Image Pause_Button_Image;
  public GameObject YouLose;
  

  //RESTART FUNCTIONS TO UPDATE THE SLIDERS
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

    if(ResetData) ResetData.SetActive(false);
    if(Restart) Restart.SetActive(false);
    if(Surrender) Surrender.SetActive(false);

    Exit.SetActive(false);
    StopAllCoroutines();
  }

  public void ToggleSound()
  {
    isSoundMuted = !isSoundMuted;
    AudioListener.pause = isSoundMuted;
    MuteButtonImage.sprite = isSoundMuted ? SoundOffSprite : SoundOnSprite;
    SoundButton_text.text = isSoundMuted ? "Sound OFF" : "Sound ON";
  }

  public void RestartPressed()
    {
        SceneManager.LoadScene("JigsawPuzz");
    }

  public void ResetDataPressed()
  {
    // Reset the sliders and player data
    if (playerProgress != null)
    {
      playerProgress.StressSlider.value = 0f;
      playerProgress.TrustSlider.value = 0f;
      playerProgress.StressBar = 0f;
      playerProgress.TrustBar = 0f;
    }
    
    // Save the reset data
    SaveData.SavePlayer(0f, 0f);
    Debug.Log("Player data has been reset to zero");
  }

  public void SurrenderPressed()
  {

    Pause_Button.SetActive(false);
    Show_Pause_Menu.SetActive(false);
    YouLose.SetActive(true);
    CinemachineShake.Instance.ShakeCamera(5f, .1f);

    Time.timeScale = 0;

    MoveTrustPosition.anchoredPosition = new Vector2(-968f, 619f);
    MoveStressPosition.anchoredPosition = new Vector2(994f, -583f);
    
    CheckCongratulation.SetActive(true);

    TrustReward.value += 0;
    TrustTextPoints.text = "+0";
    TrustTextPoints.color = Color.gray;

    StressReward.value += 20;
    StressTextPoints.text = "+20";

    SaveData.SavePlayer(TrustReward.value, StressReward.value);
  }

  public void ExitPressed()
  {
    // Save the current player progress before exiting
    if (playerProgress != null)
    {
      playerProgress.SavePlayer(playerProgress.TrustBar, playerProgress.StressBar);
    }
    else
    {
      SaveData.SavePlayer(0f, 0f);
    }
    
    Time.timeScale = 1; // Reset time scale before exiting
    Application.Quit();
    Debug.Log("Exit");

    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #endif

  }

  void Start()
  {

    Pause_Button_Image = Pause_Button.GetComponent<Image>();
    // Get the PlayerProgress component from the scene
    playerProgress = FindObjectOfType<PlayerProgress>();
    
    // Initialize the mute button image
    if (Mute != null)
    {
      MuteButtonImage = Mute.GetComponent<Image>();
    }
    
    // Load the sound sprites (SoundOff and default sprite)
    if (MuteButtonImage != null && MuteButtonImage.sprite != null)
    {
      SoundOnSprite = MuteButtonImage.sprite;
    }
  }

  void Update()
  {

    if(CheckCongratulation != null && CheckCongratulation.activeSelf)
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

    if(ResetData) ResetData.SetActive(true);
    if(Restart) Restart.SetActive(true);
    if(Exit) Exit.SetActive(true);
    if(Surrender) Surrender.SetActive(true);
  }

    void OnDisable()
    {
        Time.timeScale = 1; // Ensure time scale is reset when the Restart is pressed
        if(TrustTextPoints != null) TrustTextPoints.color = Color.green;
    }
}
