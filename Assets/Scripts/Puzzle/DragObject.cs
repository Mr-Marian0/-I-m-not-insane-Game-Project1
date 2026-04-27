using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 dragOffset;
    private Camera mainCamera;
    
    public GameObject CheckCongratulation;
    
    // Touch detection variables
    private int touchId = -1;
    private bool isDragging = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Handle touch input for Android
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(touch);
                    break;
                    
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isDragging && touch.fingerId == touchId)
                    {
                        OnTouchDrag(touch);
                    }
                    break;
                    
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == touchId)
                    {
                        OnTouchEnded();
                    }
                    break;
            }
        }
        
        // Handle mouse input for editor testing
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        
        if (Input.GetMouseButton(0) && isDragging)
        {
            OnMouseDrag();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    void OnTouchBegan(Touch touch)
    {
        if (CheckCongratulation.activeSelf) return;
        
        Vector2 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
        Collider2D hitCollider = Physics2D.OverlapPoint(touchPos);
        
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            isDragging = true;
            touchId = touch.fingerId;
            
            // Calculate offset between object position and touch position
            dragOffset = transform.position - new Vector3(touchPos.x, touchPos.y, 0);
        }
    }

    void OnTouchDrag(Touch touch)
    {
        if (CheckCongratulation.activeSelf) return;
        
        Vector3 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
        Vector3 targetPosition = new Vector3(touchPos.x, touchPos.y, 0) + dragOffset;
        
        // Keep the object at the same Z position
        targetPosition.z = transform.position.z;
        
        rb.MovePosition(targetPosition);
    }

    void OnTouchEnded()
    {
        isDragging = false;
        touchId = -1;
        rb.velocity = Vector2.zero; // Stop any residual movement
    }

    void OnMouseDown()
    {
        if (CheckCongratulation.activeSelf) return;
        
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPos);
        
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            isDragging = true;
            
            // Calculate offset between object position and mouse position
            dragOffset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        }
    }

    void OnMouseDrag()
    {
        if (CheckCongratulation.activeSelf) return;
        
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0) + dragOffset;
        
        // Keep the object at the same Z position
        targetPosition.z = transform.position.z;
        
        rb.MovePosition(targetPosition);
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.velocity = Vector2.zero;
    }
}