using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{

  public RectTransform Back2;
  float yScale = 0;
  bool Signal1 = false;
  public GameObject Show_Pause_Menu;
  public GameObject Mute;
  public GameObject ResetData;
  public GameObject Exit;
  public GameObject Pause_Button;
  public Image Pause_Button_Image;
  
  private bool isSoundMuted = false;
  private Image MuteButtonImage;
  public Sprite SoundOnSprite;
  public Sprite SoundOffSprite;
  public TextMeshProUGUI SoundButton_text;
  private PlayerProgress playerProgress;

  public void PausePressed()
  {

    Pause_Button_Image = Pause_Button.GetComponent<Image>();
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
    ResetData.SetActive(false);
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
    if (Signal1 == true)
    {
      if (yScale >= 1)
      {
        yScale = 0;
        Signal1 = false;
      }
      else
      {
        Debug.Log(Time.unscaledDeltaTime);
        yScale += Time.unscaledDeltaTime * 2;
        Back2.localScale = new Vector3(Back2.localScale.x, yScale, Back2.localScale.z);

      }
    }
  }
  IEnumerator ActivateDelay(float delay)
  {
    yield return new WaitForSecondsRealtime(delay);
    Mute.SetActive(true);
    ResetData.SetActive(true);
    Exit.SetActive(true);
  }
}
