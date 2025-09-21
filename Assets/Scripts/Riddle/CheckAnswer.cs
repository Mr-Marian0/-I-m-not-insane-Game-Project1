using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.TMP_Compatibility;

public class CheckAnswer : MonoBehaviour
{

    public RandomQuestion InheritRandomQuestion;
    public Door1Collider Choice1;
    public Door2Collider Choice2;
    public Door3Collider Choice3;
    public GameObject Door1Col;
    public GameObject Door2Col;
    public GameObject Door3Col;
    public int Mistakes = 0; //DONT FORGET BIMBO TO SET THIS BACK TO 0

    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressTextPoints;
    public Slider TrustReward;
    public Slider StressReward;
    //Enable Congratulation
    public GameObject Congratulation;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject Confetti;

    //Move Stress and Trust
    public RectTransform MoveStressPos;
    public RectTransform MoveTrustPos;

    public EnemyScript InheritEnemyScript;

    int PresentPosition;
    //Take the Answer key from RandomQuestion(Script)
    int RealAnswer;

    void Update()
    {

        if (Choice1.PlayerChooseDoor1 == true)
        {
            PresentPosition = 0;
           // Debug.Log("Present Position is: 111111111111");
        }
        else if(Choice2.PlayerChooseDoor2 == true)
        {
            PresentPosition = 1;
            //Debug.Log("Present Position is: 22222222222");
        }
        else if(Choice3.PlayerChooseDoor3 == true)
        {
            PresentPosition = 2;
            //Debug.Log("Present Position is: 333333333333");
        }
        
    }
    
    public void CheckTheAnswer()
    {
        //Check if your A.N.S.W.E.R is correct
        RealAnswer = InheritRandomQuestion.GenerateQuestion;
        if (InheritRandomQuestion.AnswerKey50[RealAnswer] == PresentPosition)
        {
            Debug.Log("CORRECT!!");

            Congratulation.SetActive(true);
            YouWin.SetActive(true);
            Confetti.SetActive(true);

            Door1Col.SetActive(false);
            Door2Col.SetActive(false);
            Door3Col.SetActive(false);

        if(Congratulation.activeSelf == true){
           if(Mistakes == 0){
             TrustReward.value += 10;
             TrustTextPoints.text = "+10";
           }
           else if(Mistakes == 1){
             StressReward.value += 2;
             StressTextPoints.text = "+2";
           }
           else if(Mistakes >= 2){
             StressReward.value += 10;
             StressTextPoints.text = "+10";
           }
        }

            MoveStressPos.anchoredPosition = new Vector2(996.23f, -584f);
            MoveTrustPos.anchoredPosition = new Vector2(-973f, 614f);
        }
        else
        {
            //TO BE CONTINUED!!!!!!!!!!!!!!
            Debug.Log("WRONG!!!");
            InheritEnemyScript.speed = 2f;
            CinemachineShake.Instance.ShakeCamera(5f, .1f);
            Mistakes += 1;

            Congratulation.SetActive(true);
            YouLose.SetActive(true);
            Confetti.SetActive(true);

            Door1Col.SetActive(false);
            Door2Col.SetActive(false);
            Door3Col.SetActive(false);

            if(Congratulation.activeSelf == true){
           if(Mistakes == 0){
             TrustReward.value += 0;
             TrustTextPoints.text = "+0";
           }
           else if(Mistakes == 1){
             StressReward.value += 5;
             StressTextPoints.text = "+5";
           }
           else if(Mistakes >= 2){
             StressReward.value += 10;
             StressTextPoints.text = "+10";
           }
        }

        }

        
    }

    public void OnDisable(){
     InheritEnemyScript.speed = 1.5f;
     
    }

}
