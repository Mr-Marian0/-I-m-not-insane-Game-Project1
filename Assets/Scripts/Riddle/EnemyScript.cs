using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public Transform PlayerPosition;
    [SerializeField] public float speed = 1.5f;
    float Xposition;

    public GameObject Congratulation;
    public GameObject YouLose;
    public GameObject Confetti;
    public GameObject STOP_PLAYER;
    public GameObject Door1Col;
    public GameObject Door2Col;
    public GameObject Door3Col;
    public GameObject PauseButton;
    public GameObject PauseCanvas;
    public GameObject JoyStickDisable;

    public Slider TrustReward;
    public TextMeshProUGUI TrustTextPoints;
    public Slider StressReward;
    public TextMeshProUGUI StressTextPoints;
    public GameObject Enemy1;

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

    int ToggleOnce = 0;
    bool EnemySpaned = false;

    public GameObject EnemyObject;
    public RandomQuestion InheritRandomQuestion;

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

    void Start()
    {
        Xposition = Random.Range(-7.66f, 7.41f);
    }

    void Update()
    {
        if (keepBarsAtTarget)
            MoveBarsToTarget();

        if (InheritRandomQuestion.SpawnEnemy == true)
        {
            if (ToggleOnce == 0)
            {
                SpawnEnemy();
                EnemySpaned = true;
                ToggleOnce += 1;
            }

            if (EnemySpaned == true)
                transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.transform.position, speed * Time.deltaTime);
        }
    }

    void SpawnEnemy()
    {
        transform.position = new Vector2(Xposition, -3.38f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CinemachineShake.Instance.ShakeCamera(5f, .1f);

        if (collision.gameObject.CompareTag("Player"))
        {

            Congratulation.SetActive(true);
            YouLose.SetActive(true);
            Confetti.SetActive(true);

            Door1Col.SetActive(false);
            Door2Col.SetActive(false);
            Door3Col.SetActive(false);
            JoyStickDisable.SetActive(false);
            PauseButton.SetActive(false);
            PauseCanvas.SetActive(false);
            STOP_PLAYER.SetActive(false);

            keepBarsAtTarget = true;

            if (SessionData.Instance != null)
                SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
            
            if (Congratulation.activeSelf)
            {
                TrustReward.value += 0;
                TrustTextPoints.text = "+0";
                TrustTextPoints.color = Color.gray;

                StressReward.value += 20;
                StressTextPoints.text = "+20";
            }

        }
    }

    public void OnEnable()
    {
        keepBarsAtTarget = false;
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        TrustTextPoints.color = Color.green;
    }

    public void OnDisable()
    {
        ResetBarsPosition();
    }
}