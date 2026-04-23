using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutoSnap : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(0.32f, 0.50f);
    
    [Header("Audio Settings")]
    public AudioSource woodMovedAudioSource; // Just drag your WoodMoved1 object here
    
    private Vector3 lastSnappedPosition;
    private float soundCooldown = 0f;

    void Start()
    {
        lastSnappedPosition = transform.localPosition;
        
        // Only try to find it if you forgot to drag it in the inspector
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
        Vector3 localPos = transform.localPosition;
        Vector3 snappedPos = new Vector3(
            Mathf.Round(localPos.x / gridSize.x) * gridSize.x,
            Mathf.Round(localPos.y / gridSize.y) * gridSize.y,
            localPos.z
        );
        
        transform.localPosition = snappedPos;
        
        if (snappedPos != lastSnappedPosition)
        {
            if (soundCooldown <= 0f)
            {
                // Just play the sound from your existing AudioSource
                if (woodMovedAudioSource != null)
                {
                    woodMovedAudioSource.Play();
                }
                soundCooldown = 0.1f;
            }
            lastSnappedPosition = snappedPos;
        }
        
        if (soundCooldown > 0f)
        {
            soundCooldown -= Time.deltaTime;
        }
    }
}