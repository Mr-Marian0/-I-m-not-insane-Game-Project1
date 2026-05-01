using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class FullscreenImageFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform canvasRect;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Climb up the hierarchy to find the root Canvas
        Canvas rootCanvas = GetComponentInParent<Canvas>();
        if (rootCanvas != null)
            canvasRect = rootCanvas.GetComponent<RectTransform>();

        Fit();
    }

    private void Fit()
    {
        if (canvasRect == null)
        {
            Debug.LogWarning("FullscreenImageFitter: No Canvas found in parents.");
            return;
        }

        // Match the exact width and height of the Canvas
        rectTransform.sizeDelta = new Vector2(canvasRect.rect.width, canvasRect.rect.height);
    }
}