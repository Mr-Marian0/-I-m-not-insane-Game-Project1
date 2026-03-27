using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void PausePressed(){
      
      Pause_Button_Image = Pause_Button.GetComponent<Image>();
      Pause_Button_Image.enabled = false;

      Time.timeScale = 0;
      Show_Pause_Menu.SetActive(true);
      Signal1 = true;
      StartCoroutine(ActivateDelay(0.5f));
      
  }

  IEnumerator ActivateDelay(float delay)
  {
    yield return new WaitForSecondsRealtime(delay);
    Mute.SetActive(true);  
    ResetData.SetActive(true);
    Exit.SetActive(true);
  }

  public void Continue(){

    Pause_Button_Image.enabled = true;
    Time.timeScale = 1;
    Pause_Button.SetActive(true);
    Show_Pause_Menu.SetActive(false);
    Mute.SetActive(false);  
    ResetData.SetActive(false);
    Exit.SetActive(false);
    
  }

    void Start()
    {
        
    }

    void Update()
    {
        if(Signal1 == true){
      if(yScale >= 1){
        yScale = 0;
        Signal1 = false;
      } else {
        Debug.Log(Time.unscaledDeltaTime);
        yScale += Time.unscaledDeltaTime*2;
        Back2.localScale = new Vector3(Back2.localScale.x, yScale, Back2.localScale.z);
        
      }
    }
    }
}
