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
    public GameObject YouLoseReference;
    public GameObject ConfettiReference;
    public PuzzleReward PuzzleRewardReference;
    public TextMeshProUGUI StressTextPoints;
    public TextMeshProUGUI TrustTextPoints;
    public Slider TrustReward;
    public Slider StressReward;
    public float TimeLimit;
    public int minutes;
    public int seconds;

    [Header("Bars Parent & Target")]
    public RectTransform barsParent;
    public RectTransform choiceTargetPosBars;

    private Vector2 originalBarsAnchoredPos;
    private Vector2 originalBarsSizeDelta;
    private Vector2 originalBarsAnchorMin;
    private Vector2 originalBarsAnchorMax;
    private Vector2 originalBarsPivot;

    private GridLayoutGroup barsGrid;
    private Vector2 originalCellSize;
    private GridLayoutGroup.Constraint originalConstraint;
    private int originalConstraintCount;

    private readonly Vector2 choiceCellSize = new Vector2(196.8f, 51.6f);
    private const GridLayoutGroup.Constraint choiceConstraint = GridLayoutGroup.Constraint.FixedRowCount;

    // When true, Update() keeps applying the target position every frame
    // so Unity's layout rebuild cannot override it
    private bool keepBarsAtTarget = false;

    public bool PauseTheTimer = true;

    private void Awake()
    {
        if (barsParent != null)
        {
            originalBarsAnchoredPos = barsParent.anchoredPosition;
            originalBarsSizeDelta = barsParent.sizeDelta;
            originalBarsAnchorMin = barsParent.anchorMin;
            originalBarsAnchorMax = barsParent.anchorMax;
            originalBarsPivot = barsParent.pivot;

            barsGrid = barsParent.GetComponent<GridLayoutGroup>();
            if (barsGrid != null)
            {
                originalCellSize = barsGrid.cellSize;
                originalConstraint = barsGrid.constraint;
                originalConstraintCount = barsGrid.constraintCount;
            }
        }
    }

    private void MoveBarsToTarget()
    {
        if (barsParent == null || choiceTargetPosBars == null) return;

        barsParent.anchorMin = choiceTargetPosBars.anchorMin;
        barsParent.anchorMax = choiceTargetPosBars.anchorMax;
        barsParent.pivot = choiceTargetPosBars.pivot;
        barsParent.anchoredPosition = choiceTargetPosBars.anchoredPosition;

        if (barsGrid != null)
        {
            barsGrid.cellSize = choiceCellSize;
            barsGrid.constraint = choiceConstraint;
            barsGrid.constraintCount = 2;
        }
    }

    private void ResetBarsPosition()
    {
        if (barsParent == null) return;

        keepBarsAtTarget = false;

        barsParent.anchorMin = originalBarsAnchorMin;
        barsParent.anchorMax = originalBarsAnchorMax;
        barsParent.pivot = originalBarsPivot;
        barsParent.anchoredPosition = originalBarsAnchoredPos;
        barsParent.sizeDelta = originalBarsSizeDelta;

        if (barsGrid != null)
        {
            barsGrid.cellSize = originalCellSize;
            barsGrid.constraint = originalConstraint;
            barsGrid.constraintCount = originalConstraintCount;
        }
    }

    void Update()
    {
        // Keep applying target position every frame to fight layout rebuilds
        if (keepBarsAtTarget)
            MoveBarsToTarget();

        if (PauseTheTimer)
        {
            minutes = Mathf.FloorToInt(TimeLimit / 60);
            seconds = Mathf.FloorToInt(TimeLimit % 60);
            TimerMission.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (TimeLimit > 0)
        {
            TimeLimit -= Time.deltaTime;
        }
        else if (TimeLimit < 0)
        {
            TimeLimit = 0;
            TimerMission.color = Color.red;
            TimerEnded();
        }
    }

    private void TimerEnded()
    {
        keepBarsAtTarget = true;
        ConvertTimeToReward(0);
        ActivateCongratulation();

        if(SessionData.Instance != null)
        {
            SessionData.Instance.stressBar.value = StressReward.value;
            SessionData.Instance.trustBar.value = TrustReward.value;
        }
    }

    private void ActivateCongratulation()
    {
        CongratulationReference.SetActive(true);
        YouLoseReference.SetActive(true);
        ConfettiReference.SetActive(true);
    }

    public void ConvertTimeToReward(int sec)
    {
        TrustReward.value += 0;
        TrustTextPoints.text = "+0";

        StressReward.value += 30;
        StressTextPoints.text = "+30";
    }

    public void OnEnable()
    {
        TimeLimit = 60;
        keepBarsAtTarget = false;
    }

    public void OnDisable()
    {
        TimerMission.color = Color.white;
        ResetBarsPosition();
    }
}