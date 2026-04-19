using UnityEngine;
using System.Collections;

public class PlayerHitEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    
    public void FlashRed(float duration)
    {
        StartCoroutine(FlashCoroutine(duration));
    }
    
    IEnumerator FlashCoroutine(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}