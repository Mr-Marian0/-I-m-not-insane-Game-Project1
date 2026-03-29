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

    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressTextPoints;
    public Slider TrustReward;
    public Slider StressReward;
    //Enable Congratulation
    public GameObject Congratulation;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject Confetti;
    public GameObject Enemy1;

    //Move Stress and Trust
    public RectTransform MoveTrustPosition;
    public RectTransform MoveStressPosition;
    public Vector3 TrustDefaultPosXY;
    public Vector3 StressDefaultPosXY;

    public EnemyScript InheritEnemyScript;

    int PresentPosition;
    //Take the Answer key from RandomQuestion(Script)
    int RealAnswer;

    private void Start()
    {
        // Load saved values into the sliders so rewards increment properly
        PlayerData data = SaveData.LoadPlayer();
        if (data != null)
        {
            TrustReward.value = data.TrustData;
            StressReward.value = data.StressData;
            Debug.Log("Loaded saved values - Trust: " + data.TrustData + ", Stress: " + data.StressData);
        }
    }

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
                TrustReward.value += 10;
                TrustTextPoints.text = "+10";

                StressReward.value -= 10;
                StressTextPoints.text = "-10";
                StressTextPoints.color = Color.green;

                Enemy1.SetActive(false);
        }

            MoveStressPosition.anchoredPosition = new Vector2(996.23f, -584f);
            MoveTrustPosition.anchoredPosition = new Vector2(-973f, 614f);
            
            // Save the reward values
            SaveData.SavePlayer(TrustReward.value, StressReward.value);
            Debug.Log("Riddle completed - saved Trust: " + TrustReward.value + ", Stress: " + StressReward.value);
        }
        else
        {
            Debug.Log("WRONG!!!");
            InheritEnemyScript.speed = 2f;
            CinemachineShake.Instance.ShakeCamera(5f, .1f);

            Congratulation.SetActive(true);
            YouLose.SetActive(true);
            Confetti.SetActive(true);

            Door1Col.SetActive(false);
            Door2Col.SetActive(false);
            Door3Col.SetActive(false);

            if(Congratulation.activeSelf == true){
                TrustReward.value += 0;
                TrustTextPoints.text = "+0";
                TrustTextPoints.color = Color.gray;

                StressReward.value += 20;
                StressTextPoints.text = "+20";

                Enemy1.SetActive(false);
            }

            MoveStressPosition.anchoredPosition = new Vector2(996.23f, -584f);
            MoveTrustPosition.anchoredPosition = new Vector2(-973f, 614f);
        }

        // Save the reward values
            SaveData.SavePlayer(TrustReward.value, StressReward.value);
    }

    public void OnEnable()
    {
        //RESET
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        StressTextPoints.color = Color.red;
        TrustTextPoints.color = Color.green;
    }

    public void OnDisable(){
        InheritEnemyScript.speed = 1.5f;
    }

}
