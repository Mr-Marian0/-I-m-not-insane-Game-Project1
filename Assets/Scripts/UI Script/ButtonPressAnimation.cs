using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPressAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Animation Settings")]
    [SerializeField] private float pressScale = 0.90f;
    [SerializeField] private float animationDuration = 0.1f;
    
    private Vector3 originalScale;
    private bool isPressed = false;
    
    private void Awake()
    {
        originalScale = transform.localScale;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPressed) return;
        isPressed = true;
        
        // Scale down
        transform.localScale = originalScale * pressScale;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPressed) return;
        isPressed = false;
        
        // Scale back up
        transform.localScale = originalScale;
    }
    
    // Optional: Also handle when pointer leaves button area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPressed)
        {
            transform.localScale = originalScale;
            isPressed = false;
        }
    }
}