using UnityEngine;

public class AutoScaleSprite : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleToScreen();
    }
    
    void ScaleToScreen()
    {
        // Get camera corners
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 10));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        
        float cameraWidth = topRight.x - bottomLeft.x;
        float cameraHeight = topRight.y - bottomLeft.y;
        
        // Get original sprite size
        Vector2 originalSize = spriteRenderer.sprite.bounds.size;
        
        // Calculate scale to fit camera
        float scaleX = cameraWidth / originalSize.x;
        float scaleY = cameraHeight / originalSize.y;
        
        // Apply scale
        transform.localScale = new Vector3(scaleX, scaleY, 1);
        
        // Center in camera
        transform.position = new Vector3(bottomLeft.x + cameraWidth/2, bottomLeft.y + cameraHeight/2, 0);
    }
    
    void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            lastWidth = Screen.width;
            lastHeight = Screen.height;
            ScaleToScreen();
        }
    }
    
    private int lastWidth;
    private int lastHeight;
}