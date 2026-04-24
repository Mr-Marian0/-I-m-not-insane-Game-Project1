using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{

    public FixedJoystick Joystick;
    Rigidbody2D rb;
    Vector2 move;
    public float moveSpeed;
    
    // Audio variables
    public AudioSource audioSource;
    public AudioClip walkingSound;
    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Get or add AudioSource component
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        // Configure AudioSource for best performance
        audioSource.loop = true;  // Walking sound should loop
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;  // 2D sound
    }

    void Update()
    {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;
        
        // Check if player is moving
        bool isMoving = (Mathf.Abs(move.x) > 0.1f || Mathf.Abs(move.y) > 0.1f);
        
        // Handle walking sound
        if (isMoving && !isWalking)
        {
            // Started walking
            isWalking = true;
            PlayWalkingSound();
        }
        else if (!isMoving && isWalking)
        {
            // Stopped walking
            isWalking = false;
            StopWalkingSound();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
    
    void PlayWalkingSound()
    {
        if (walkingSound != null && audioSource != null)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
    }
    
    void StopWalkingSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}