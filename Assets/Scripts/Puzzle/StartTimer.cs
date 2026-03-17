using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using static TMPro.TMP_Compatibility;

public class StartTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerMission;
    public GameObject CongratulationReference;
    public GameObject PuzzleReference;
    public GameObject YouWinReference;
    public GameObject ConfettiReference;
    public PuzzleReward PuzzleRewardReference;
    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressTextPoints;
    public Slider TrustReward;
    public Slider StressReward;
    public float TimeLimit;
    public int minutes;
    public int seconds;

    //TRUST RECT TRANSFORM POSITIONS - REPLACE TO CONGRATULATION - POSITIONS
    public RectTransform MoveTrustPosition;

    //STRESS RECT TRANSFORM POSITIONS - REPLACE TO CONGRATULATION - POSITIONS
    public RectTransform MoveStressPosition;

    public bool PauseTheTimer = true;
   
    void Update()
    {

        //PAUSE
        if(PauseTheTimer)
        {
            minutes = Mathf.FloorToInt(TimeLimit / 60);
            seconds = Mathf.FloorToInt(TimeLimit % 60);
            TimerMission.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        //RESET


        //TIMER DURING CHALLENGE
        if (TimeLimit > 0)
        {
            TimeLimit -= Time.deltaTime;
        }
        // DETECT IF THE TIMER ENDS
        else if (TimeLimit < 0)
        {
            TimeLimit = 0;
            TimerMission.color = Color.red;
            ActivateCongratulation();
            YouWinReference.SetActive(true);
            ConfettiReference.SetActive(true);
        }
        
    }

    public void OnEnable()
    {
        TimeLimit = 60;
    }

    public void OnDisable(){
        TimerMission.color = Color.white;
    }

    void ActivateCongratulation()
    {
        foreach (Transform puzz in PuzzleReference.transform)
        {
            if (puzz.gameObject.activeSelf)
            {
                
                MoveTrustPosition.anchoredPosition = new Vector2(-968f, 619f);
                MoveStressPosition.anchoredPosition = new Vector2(994f, -583f);

                ConvertTimeToReward(0);
                CongratulationReference.SetActive(true);
                
                break;
            }
        }
    }

    public void ConvertTimeToReward(int sec)
    {
        
        if (sec >= 50 && sec <= 60)
        {
            TrustReward.value += 10;
            TrustTextPoints.text = "+10";

            StressReward.value -= 7;
            StressTextPoints.text = "-5";
            StressTextPoints.color = Color.green;
        }
        else if(sec >= 40 && sec <= 49)
        {
            TrustReward.value += 7;
            TrustTextPoints.text = "+7";

            TrustReward.value -= 5;
            StressTextPoints.text = "-5";
            StressTextPoints.color = Color.green;
        }
        else if(sec >= 30 && sec <= 39)
        {
            TrustReward.value += 3;
            TrustTextPoints.text = "+5";

            StressReward.value += 5;
            StressTextPoints.text = "+5";
        }
        else if(sec <= 29)
        {
            TrustReward.value += 1;
            TrustTextPoints.text = "+1";

            StressReward.value += 1;
            StressTextPoints.text = "+1";
        }
    }
}
