using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour
{

    public Transform PlayerPosition;
    [SerializeField]public float speed = 1.5f;
    float Xposition;

    //IF PLAYER TOUCHES THE ENEMY
    public GameObject Congratulation;
    public GameObject YouLose;
    public GameObject Confetti;
    public GameObject STOP_PLAYER;
    public GameObject Door1Col;
    public GameObject Door2Col;
    public GameObject Door3Col;
    public GameObject PauseButton;
    public GameObject PauseCanvas;

    //UI and game state references
    public Slider TrustReward;
    public TextMeshProUGUI TrustTextPoints;
    public Slider StressReward;
    public TextMeshProUGUI StressTextPoints;
    public GameObject Enemy1;

    //Move Stress and Trust
    public RectTransform MoveStressPosition;
    public RectTransform MoveTrustPosition;
    public Vector3 TrustDefaultPosXY;
    public Vector3 StressDefaultPosXY;

    //Run it once
    int ToggleOnce = 0;
    bool EnemySpaned = false;

    //Enemy to spawn
    public GameObject EnemyObject;

    //Check if the BackgroundMove is finished
    public RandomQuestion InheritRandomQuestion;

    void Start()
    {
        Xposition = Random.Range(-7.66f, 7.41f);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (InheritRandomQuestion.SpawnEnemy == true)
        {
            
            if (ToggleOnce == 0)
            {
                SpawnEnemy();
                EnemySpaned = true;
                ToggleOnce += 1;

            }

            if(EnemySpaned == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.transform.position, speed * Time.deltaTime);
            }
            
        }

    }

    void SpawnEnemy()
    {
        //-7.66 -- 7.41, y = -3.38

        transform.position = new Vector2(Xposition, -3.38f);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            CinemachineShake.Instance.ShakeCamera(5f, .1f);
            if (collision.gameObject.CompareTag("Player")) {
                Debug.Log("ENEMY COLLIDES WITH PLAYER");

                Congratulation.SetActive(true);
                YouLose.SetActive(true);
                Confetti.SetActive(true);

                Door1Col.SetActive(false);
                Door2Col.SetActive(false);
                Door3Col.SetActive(false);
                PauseButton.SetActive(false);
                PauseCanvas.SetActive(false);

                STOP_PLAYER.SetActive(false);

                if(Congratulation.activeSelf == true){
                TrustReward.value += 0;
                TrustTextPoints.text = "+0";
                TrustTextPoints.color = Color.gray;

                StressReward.value += 20;
                StressTextPoints.text = "+20";

                Enemy1.SetActive(false);
            }

            MoveTrustPosition.anchoredPosition = new Vector2(-8.8f, -261.2f);
            MoveStressPosition.anchoredPosition = new Vector2(474.6f, -325.8f);

            // Save the reward values

            if (SessionData.Instance != null)
            {
                SessionData.Instance.UpdateBars(TrustReward.value, StressReward.value);
            }
            }
        }

        public void OnEnable()
    {
        //RESET
        TrustTextPoints.text = "";
        StressTextPoints.text = "";
        TrustTextPoints.color = Color.green;
    }

    public void OnDisable(){
        if (MoveTrustPosition != null)
        {
            MoveTrustPosition.anchoredPosition = TrustDefaultPosXY;
        }
        if (MoveStressPosition != null)
        {
            MoveStressPosition.anchoredPosition = StressDefaultPosXY;
        }
    }
}
