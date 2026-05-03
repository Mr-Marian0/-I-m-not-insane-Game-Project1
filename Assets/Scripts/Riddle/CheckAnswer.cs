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
    public GameObject PauseButton;
    public GameObject PauseCanvas;

    public TextMeshProUGUI TrustTextPoints;
    public TextMeshProUGUI StressTextPoints;
    public Slider TrustReward;
    public Slider StressReward;

    public GameObject Congratulation;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject Confetti;
    public GameObject Enemy1;
    public GameObject JoyStickReference;

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

    private bool keepBarsAtTarget = false;

    public EnemyScript InheritEnemyScript;

    int PresentPosition = 4;
    int RealAnswer;

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

    private void Start()
    {
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
        if (keepBarsAtTarget)
            MoveBarsToTarget();

        if (Choice1.PlayerChooseDoor1 == true)
            PresentPosition = 0;
        else if (Choice2.PlayerChooseDoor2 == true)
            PresentPosition = 1;
        else if (Choice3.PlayerChooseDoor3 == true)
            PresentPosition = 2;
    }

    public void CheckTheAnswer()
    {
    RealAnswer = InheritRandomQuestion.GenerateQuestion;

    if (InheritRandomQuestion.AnswerKey50[RealAnswer] == PresentPosition)
    {
        CorrectAnswer();
    }
    else if (PresentPosition == 4 || Choice1.PlayerChooseDoor1 == false && Choice2.PlayerChooseDoor2 == false && Choice3.PlayerChooseDoor3 == false)
    {
        NoDoorChosen();
    }
    else
    {
        WrongAnswer();
    }

    // Always runs after any outcome — start keeping bars at target every frame
    keepBarsAtTarget = true;
    Debug.Log("keepBarsAtTarget set to true");

    if (SessionData.Instance != null)
        SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
    }

    private void CorrectAnswer()
    {
        Debug.Log("CORRECT!!");

        ShowCongratulation();
        YouWin.SetActive(true);

        if (Congratulation.activeSelf)
        {
            TrustReward.value += 10;
            TrustTextPoints.text = "+10";

            StressReward.value -= 10;
            StressTextPoints.text = "-10";
            StressTextPoints.color = Color.green;

            Enemy1.SetActive(false);
            PauseButton.SetActive(false);
            PauseCanvas.SetActive(false);
            JoyStickReference.SetActive(false);
        }

        keepBarsAtTarget = true;

        if (SessionData.Instance != null)
            SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
    }

    private void NoDoorChosen()
    {
        CinemachineShake.Instance.ShakeCamera(5f, .1f);

        ShowCongratulation();
        YouLose.SetActive(true);

        if (Congratulation.activeSelf)
        {
            TrustReward.value += 0;
            TrustTextPoints.text = "+0";
            TrustTextPoints.color = Color.gray;

            StressReward.value += 20;
            StressTextPoints.text = "+20";

            Enemy1.SetActive(false);
            PauseButton.SetActive(false);
            PauseCanvas.SetActive(false);
            JoyStickReference.SetActive(false);
        }

        keepBarsAtTarget = true;

        if (SessionData.Instance != null)
            SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
    }

    private void WrongAnswer()
    {
        Debug.Log("WRONG!!!");
        InheritEnemyScript.speed = 2f;
        CinemachineShake.Instance.ShakeCamera(5f, .1f);

        ShowCongratulation();
        YouLose.SetActive(true);

        PauseButton.SetActive(false);
        PauseCanvas.SetActive(false);
        JoyStickReference.SetActive(false);

        if (Congratulation.activeSelf)
        {
            TrustReward.value += 0;
            TrustTextPoints.text = "+0";
            TrustTextPoints.color = Color.gray;

            StressReward.value += 20;
            StressTextPoints.text = "+20";

            Enemy1.SetActive(false);
        }

        keepBarsAtTarget = true;
    }

    private void ShowCongratulation()
    {
        Congratulation.SetActive(true);
        Confetti.SetActive(true);
        Door1Col.SetActive(false);
        Door2Col.SetActive(false);
        Door3Col.SetActive(false);
    }

    public void OnEnable()
    {
        keepBarsAtTarget = false;
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        StressTextPoints.color = Color.red;
        TrustTextPoints.color = Color.green;
    }

    public void OnDisable()
    {
        InheritEnemyScript.speed = 1.5f;
        ResetBarsPosition();
    }
}