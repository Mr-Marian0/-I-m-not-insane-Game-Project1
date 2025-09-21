using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 mousePosition;
    public Vector3 worldPosition;

    public GameObject CheckCongratulation;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    public void Update()
    {
        mousePosition = Input.mousePosition;

        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

     public void OnMouseDrag()
    {
       if(CheckCongratulation.activeSelf){
        
       }  else {
        rb.MovePosition(worldPosition);
       } 
    }
   
}
