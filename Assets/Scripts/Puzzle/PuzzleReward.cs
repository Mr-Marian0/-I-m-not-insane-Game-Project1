using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.TMP_Compatibility;

public class PuzzleReward : MonoBehaviour
{
    public PuzzlesKeyColliders InheritPuzzleKeyColliders;

    public TextMeshProUGUI StressTextPoints;
    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressPercentageText;
    public TextMeshProUGUI TrustPercentageText;

    [Header("Bars Parent & Target")]
    // Assign the parent GameObject that contains both stress and trust bars
    public RectTransform barsParent;
    // Assign the empty ChoiceTargetPosBars object placed where you want bars to move.
    // Set its Anchor Preset to Middle-Center in the Inspector.
    public RectTransform choiceTargetPosBars;

    // Stored original values of barsParent
    private Vector2 originalBarsAnchoredPos;
    private Vector2 originalBarsSizeDelta;
    private Vector2 originalBarsAnchorMin;
    private Vector2 originalBarsAnchorMax;
    private Vector2 originalBarsPivot;
    private readonly Vector2 choiceCellSize = new Vector2(196.8f, 51.6f);
    private const GridLayoutGroup.Constraint choiceConstraint = GridLayoutGroup.Constraint.FixedRowCount;
    public GridLayoutGroup ModifyGridWithBarsParent;

    // Slider - give player a reward
    public Slider TrustReward;
    public Slider StressReward;
    public bool FunctionCallOnce = false;

    // Remaining time after you finish the puzzle
    public StartTimer InheritStartTimer;

    // Saves or stores the time you finished the game
    int StoreTheTimeItFinished;

    private void Awake()
    {

        ModifyGridWithBarsParent = barsParent.GetComponent<GridLayoutGroup>();

        // Store all original barsParent transform values at startup
        if (barsParent != null)
        {
            originalBarsAnchoredPos = barsParent.anchoredPosition;
            originalBarsSizeDelta = barsParent.sizeDelta;
            originalBarsAnchorMin = barsParent.anchorMin;
            originalBarsAnchorMax = barsParent.anchorMax;
            originalBarsPivot = barsParent.pivot;
        }
    }

    // Moves barsParent to the target position by copying all anchor/pivot/position
    // values from choiceTargetPosBars — works correctly on any screen size
    private void MoveBarsToTarget()
    {
        if (barsParent == null || choiceTargetPosBars == null) return;

        barsParent.anchorMin = choiceTargetPosBars.anchorMin;
        barsParent.anchorMax = choiceTargetPosBars.anchorMax;
        barsParent.pivot = choiceTargetPosBars.pivot;
        barsParent.anchoredPosition = choiceTargetPosBars.anchoredPosition;
        ModifyGridWithBarsParent.cellSize = choiceCellSize;
        ModifyGridWithBarsParent.constraint = choiceConstraint;
        ModifyGridWithBarsParent.constraintCount = 2; 
    }

    // Restores barsParent to its original position
    private void ResetBarsPosition()
    {
        if (barsParent == null) return;

        barsParent.anchorMin = originalBarsAnchorMin;
        barsParent.anchorMax = originalBarsAnchorMax;
        barsParent.pivot = originalBarsPivot;
        barsParent.anchoredPosition = originalBarsAnchoredPos;
        barsParent.sizeDelta = originalBarsSizeDelta;
    }

    private void Start()
    {

        FunctionCallOnce = false;

        // Load saved values into the sliders so rewards increment properly
        PlayerData data = SaveData.LoadPlayer();
        if (data != null)
        {
            TrustReward.value = data.TrustData;
            StressReward.value = data.StressData;
            StressPercentageText.text = Mathf.RoundToInt(StressReward.value).ToString();
            TrustPercentageText.text = Mathf.RoundToInt(TrustReward.value).ToString();
        }
    }

    void Update()
    {
        if (InheritPuzzleKeyColliders.IsFinished == true)
        {
            // Move bars parent to target — screen-size safe, replaces hardcoded values
            MoveBarsToTarget();

            if (!FunctionCallOnce)
            {
                FunctionCallOnce = true;
                StoreTheTimeItFinished = InheritStartTimer.seconds;
                Debug.Log("TIMEFINISHED! :  " + StoreTheTimeItFinished);

                ConvertTimeToReward(StoreTheTimeItFinished);
                if (SessionData.Instance != null)
                {
                    SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
                }

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

            StressReward.value = 10;
            StressTextPoints.text = "-10";
            StressTextPoints.color = Color.green;
        }
        else if (sec >= 6 && sec <= 44)
        {
            TrustReward.value += 5;
            TrustTextPoints.text = "+5";

            StressReward.value += 5;
            StressTextPoints.text = "+5";
            StressTextPoints.color = Color.green;
        }
        else if (sec <= 5)
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
        FunctionCallOnce = false;
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        StressTextPoints.color = Color.red;
        TrustTextPoints.color = Color.green;
    }

    public void OnDisable()
    {
        InheritPuzzleKeyColliders.IsFinished = false;

        // Restore bars to original position when this object is disabled
        ResetBarsPosition();
    }
}