using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public RectTransform JoyStickHandleTransform;
    private SpriteRenderer rd;
    private Animator Anim;
    
    // Reference to the joystick background to get normalized values
    public RectTransform JoyStickBackground;
    private Vector2 joystickInput;

    public enum MovementState { idle, walking }
    
    void Start()
    {
        Anim = GetComponent<Animator>();
        rd = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        AnimationStateUpdate();
    }

    public void AnimationStateUpdate()
    {
        MovementState state;
        
        // Calculate the normalized joystick position (from -1 to 1)
        Vector2 joystickOffset = JoyStickHandleTransform.anchoredPosition;
        float maxRadius = JoyStickBackground.rect.width / 2;
        
        // Normalize the joystick input
        float horizontalInput = joystickOffset.x / maxRadius;
        
        // Check if there's actual input (beyond a small deadzone)
        if (Mathf.Abs(horizontalInput) > 0.1f) // Small deadzone threshold
        {
            state = MovementState.walking;
            
            // Flip sprite based on joystick direction
            if (horizontalInput < 0)
                rd.flipX = false; // Moving left
            else
                rd.flipX = true;  // Moving right
        }
        else
        {
            state = MovementState.idle;
        }
        
        Anim.SetInteger("state", (int)state);
    }
}