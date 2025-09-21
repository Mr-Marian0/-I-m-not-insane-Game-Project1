using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.TMP_Compatibility;

public class PuzzleReward : MonoBehaviour
{
    public PuzzlesKeyColliders InheritPuzzleKeyColliders;

    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressTextPoints;

    //TRUST RECT TRANSFORM POSITIONS - REPLACE TO CONGRATULATION - POSITIONS
    public RectTransform MoveTrustPosition;
    public Vector3 TrustDefaultPosXY;

    //STRESS RECT TRANSFORM POSITIONS - REPLACE TO CONGRATULATION - POSITIONS
    public RectTransform MoveStressPosition;
    public Vector3 StressDefaultPosXY;

    //SLIDER - GIVE PLAYER A REWARD
    public Slider TrustReward;
    public Slider StressReward;
    public bool FunctionCallOnce = false;

    //REMAINING TIME AFTER YOU FINISH THE PUZZLE
    public StartTimer InheritStartTimer;

    //SAVES OR STORE THE TIME YOU FINISHED THE GAME
    int StoreTheTimeItFinished;

    private void Start()
    {
        //RESET
        FunctionCallOnce = false;
    }

    void Update()
    {
        if(InheritPuzzleKeyColliders.IsFinished == true)
        {

            MoveTrustPosition.anchoredPosition = new Vector2(-968f, 619f);
            MoveStressPosition.anchoredPosition = new Vector2(994f, -583f);

            if (!FunctionCallOnce)
            {
                FunctionCallOnce = true;
                StoreTheTimeItFinished = InheritStartTimer.seconds;
                Debug.Log("TIMEFINISHED! :  " + StoreTheTimeItFinished);

                ConvertTimeToReward(StoreTheTimeItFinished);
                //RESET
                StoreTheTimeItFinished = 0;

                
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

    public void OnEnable()
    {
        //RESET
        FunctionCallOnce = false;
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        StressTextPoints.color = Color.red;
    }

    public void OnDisable()
    {
        InheritPuzzleKeyColliders.IsFinished = false;

        //RESET THE DEFAULT POSITION
        MoveTrustPosition.anchoredPosition = TrustDefaultPosXY;
        MoveStressPosition.anchoredPosition = StressDefaultPosXY;
    }
}
