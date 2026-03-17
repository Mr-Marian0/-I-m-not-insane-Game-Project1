using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractToDoor : MonoBehaviour
{

    public GameObject DoorButton;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DoorButton.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DoorButton.SetActive(false);
    }
}
