using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
  
  public RectTransform Back1;
  public GameObject Show_Stress_Trust_info;
  float yScale = 0;
  bool Signal1 = false;
  public GameObject Icon2;
  public GameObject Icon4;
  public GameObject Description;
  public GameObject Description1;

  public void Instruction(){
   Show_Stress_Trust_info.SetActive(true);
   Icon2.SetActive(true);
   Description.SetActive(true);
   Signal1 = true;

   Icon4.SetActive(false);
   Description1.SetActive(false);
  }

  public void Instruction1(){
   Icon4.SetActive(true);
   Description1.SetActive(true);

   Icon2.SetActive(false);
   Description.SetActive(false);
  }

  public void Resume1(){
    Show_Stress_Trust_info.SetActive(false);
  }

  void Update(){
    
    if(Signal1 == true){
      if(yScale >= 1){
        yScale = 0;
        Signal1 = false;
      } else {
        Debug.Log(Time.deltaTime);
        yScale += Time.deltaTime;
        Back1.localScale = new Vector3(Back1.localScale.x, yScale, Back1.localScale.z);
        
      }

     
    }
  }

  void OnDisable(){
   Icon2.SetActive(false);
   Description.SetActive(false);
   Icon4.SetActive(false);
   Description1.SetActive(false);
  }

}
