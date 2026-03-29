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

            // Save the reward values directly
            SaveData.SavePlayer(TrustReward.value, StressReward.value);
            
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
            TrustReward.value += 0;
            TrustTextPoints.text = "+0";

            StressReward.value += 30;
            StressTextPoints.text = "+30";
    }
}
