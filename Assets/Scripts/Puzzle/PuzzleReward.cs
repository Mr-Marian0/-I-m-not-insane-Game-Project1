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
        if(InheritPuzzleKeyColliders.IsFinished == true)
        {

            MoveTrustPosition.anchoredPosition = new Vector2(-14.9f, 83.8f);
            MoveStressPosition.anchoredPosition = new Vector2(-14.9f, -67.94698f);

            if (!FunctionCallOnce)
            {
                FunctionCallOnce = true;
                StoreTheTimeItFinished = InheritStartTimer.seconds;
                Debug.Log("TIMEFINISHED! :  " + StoreTheTimeItFinished);

                ConvertTimeToReward(StoreTheTimeItFinished);
                
                // Save the reward values directly
                SaveData.SavePlayer(TrustReward.value, StressReward.value);
                Debug.Log("Player data saved!");

                //RESET
                StoreTheTimeItFinished = 0;
            }
            

        }
    }

    public void ConvertTimeToReward(int sec)
    {
        
        if (sec >= 45 && sec <= 60)
        {
            TrustReward.value += 10;
            TrustTextPoints.text = "+10";

            StressReward.value -= 10;
            StressTextPoints.text = "-10";
            StressTextPoints.color = Color.green;
        }
        else if(sec >= 6 && sec <= 44)
        {
            TrustReward.value += 5;
            TrustTextPoints.text = "+5";

            StressReward.value += 5;
            StressTextPoints.text = "+5";
            StressTextPoints.color = Color.green;
        }
        else if(sec <= 5)
        {
            TrustReward.value += 0;
            TrustTextPoints.text = "+0";
            TrustTextPoints.color = Color.gray;

            StressReward.value += 20;
            StressTextPoints.text = "+20";
        }
    }

    public void SavePlayerReward()
    {
        
    }

    public void OnEnable()
    {
        //RESET
        FunctionCallOnce = false;
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        StressTextPoints.color = Color.red;
        TrustTextPoints.color = Color.green;
    }

    public void OnDisable()
    {
        InheritPuzzleKeyColliders.IsFinished = false;
    }
}
