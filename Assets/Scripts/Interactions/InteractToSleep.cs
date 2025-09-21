using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractToSleep : MonoBehaviour
{

    public EdgeCollider2D EdgeColl2d;
    public GameObject SleepButton;

    void Start()
    {
        EdgeColl2d = GetComponent<EdgeCollider2D>();
    } 

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter Trigger!");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        SleepButton.SetActive(true);
        Debug.Log("Stay");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SleepButton.SetActive(false);
    }

}
