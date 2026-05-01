using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutoSnap : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(0.32f, 0.50f);
    
    [Header("Audio Settings")]
    public AudioSource woodMovedAudioSource;
    
    private Vector3 lastSnappedPosition;
    private float soundCooldown = 0f;
    private Vector3 unsnappedPosition;
    private bool needsSnapping = false;

    void Start()
    {
        lastSnappedPosition = transform.localPosition;
        
        if (woodMovedAudioSource == null)
        {
            GameObject woodObject = GameObject.Find("WoodMoved1");
            if (woodObject != null)
            {
                woodMovedAudioSource = woodObject.GetComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        // Only apply snap when needed (after drag ends)
        if (needsSnapping)
        {
            Vector3 snappedPos = new Vector3(
                Mathf.Round(unsnappedPosition.x / gridSize.x) * gridSize.x,
                Mathf.Round(unsnappedPosition.y / gridSize.y) * gridSize.y,
                unsnappedPosition.z
            );
            
            transform.localPosition = snappedPos;
            
            if (snappedPos != lastSnappedPosition)
            {
                if (soundCooldown <= 0f)
                {
                    if (woodMovedAudioSource != null)
                    {
                        woodMovedAudioSource.Play();
                    }
                    soundCooldown = 0.1f;
                }
                lastSnappedPosition = snappedPos;
            }
            
            needsSnapping = false;
        }
        
        if (soundCooldown > 0f)
        {
            soundCooldown -= Time.deltaTime;
        }
    }
    
    // Call this method when dragging starts
    public void OnDragStart()
    {
        needsSnapping = false;
    }
    
    // Call this method when dragging ends
    public void OnDragEnd()
    {
        unsnappedPosition = transform.localPosition;
        needsSnapping = true;
    }
}