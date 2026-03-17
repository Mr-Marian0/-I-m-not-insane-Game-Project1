using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            if (collision.gameObject.CompareTag("Player")) {
                Debug.Log("ENEMY COLLIDES WITH PLAYER");

                Congratulation.SetActive(true);
                YouLose.SetActive(true);
                Confetti.SetActive(true);

                Door1Col.SetActive(false);
                Door2Col.SetActive(false);
                Door3Col.SetActive(false);

                STOP_PLAYER.SetActive(false);
                
            }
        }

}
