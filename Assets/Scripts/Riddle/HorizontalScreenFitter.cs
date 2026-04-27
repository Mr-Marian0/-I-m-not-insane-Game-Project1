using UnityEngine;

/// <summary>
/// Stretches this GameObject horizontally to match the screen's left and right world-space edges.
/// Only the X position and X localScale are ever modified — Y and Z remain untouched.
/// Attach this script directly to the GameObject that has the SpriteRenderer.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class HorizontalScreenFitter : MonoBehaviour
{
    // ──────────────────────────────────────────────────────────────
    // Inspector Settings
    // ──────────────────────────────────────────────────────────────

    [Header("Camera")]
    [Tooltip("Camera used to calculate world-space screen edges. " +
             "Leave empty to auto-use Camera.main.")]
    public Camera targetCamera;

    [Header("Stretch Settings")]
    [Tooltip("Extra world-unit padding subtracted from each horizontal side. " +
             "Use a negative value to overshoot beyond screen edges.")]
    public float horizontalPadding = 0f;

    [Tooltip("Fit the object immediately in the Editor (Edit Mode preview).")]
    public bool previewInEditor = true;

    // ──────────────────────────────────────────────────────────────
    // Private State
    // ──────────────────────────────────────────────────────────────

    private SpriteRenderer spriteRenderer;
    private int lastScreenWidth  = -1;
    private int lastScreenHeight = -1;

    // ──────────────────────────────────────────────────────────────
    // Unity Lifecycle
    // ──────────────────────────────────────────────────────────────

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResolveCamera();
    }

    private void Start()
    {
        ApplyHorizontalFit();
        CacheScreenSize();
    }

    /// <summary>
    /// LateUpdate runs after all Transforms are moved this frame,
    /// so our correction is always applied last — no jitter.
    /// </summary>
    private void LateUpdate()
    {
        if (HasScreenChanged())
        {
            ApplyHorizontalFit();
            CacheScreenSize();
        }
    }

    // ──────────────────────────────────────────────────────────────
    // Core Logic
    // ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Calculates the world-space width of the current screen and scales
    /// this object along X so that it exactly spans left-edge to right-edge
    /// (minus any padding). Repositions the object's X to the screen centre.
    /// Y and Z values of both position and scale are never touched.
    /// </summary>
    public void ApplyHorizontalFit()
    {
        if (!ValidateReferences()) return;

        // ── 1. Find screen edges in world space ──────────────────
        // We sample at this object's depth relative to the camera so the
        // calculation is correct for both perspective and orthographic cameras.
        float depth = GetDepthFromCamera();

        Vector3 leftEdge  = targetCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, depth));
        Vector3 rightEdge = targetCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth));

        float screenWorldWidth = (rightEdge.x - leftEdge.x) - (horizontalPadding * 2f);
        float screenCenterX    = (leftEdge.x  + rightEdge.x) * 0.5f;

        // ── 2. Get the sprite's native width in world units ───────
        float spriteWidth = GetSpriteWorldWidth();
        if (spriteWidth <= 0f)
        {
            Debug.LogWarning($"[HorizontalScreenFitter] '{name}': sprite width is zero or negative. Skipping.", this);
            return;
        }

        // ── 3. Compute required X scale ───────────────────────────
        // Divide by current parent scale on X so the local scale
        // we set produces the correct final world size.
        float parentScaleX  = transform.parent != null ? transform.parent.lossyScale.x : 1f;
        float requiredScaleX = screenWorldWidth / (spriteWidth * Mathf.Abs(parentScaleX));

        // ── 4. Apply — X only ─────────────────────────────────────
        // Scale: keep Y and Z exactly as they are.
        transform.localScale = new Vector3(
            requiredScaleX,
            transform.localScale.y,
            transform.localScale.z
        );

        // Position: keep Y and Z exactly as they are.
        transform.position = new Vector3(
            screenCenterX,
            transform.position.y,
            transform.position.z
        );
    }

    // ──────────────────────────────────────────────────────────────
    // Helpers
    // ──────────────────────────────────────────────────────────────

    /// <summary>Returns the Z distance from this object to the camera.</summary>
    private float GetDepthFromCamera()
    {
        if (targetCamera.orthographic)
        {
            // For orthographic cameras the depth doesn't affect world width,
            // but ViewportToWorldPoint still needs a positive Z value.
            return Mathf.Abs(transform.position.z - targetCamera.transform.position.z);
        }
        else
        {
            // Perspective: depth affects projected world size.
            Vector3 camToObject = transform.position - targetCamera.transform.position;
            return Vector3.Dot(camToObject, targetCamera.transform.forward);
        }
    }

    /// <summary>
    /// Returns the sprite's width in world units at a localScale of (1,1,1).
    /// Uses sprite.bounds which is always in local sprite space.
    /// </summary>
    private float GetSpriteWorldWidth()
    {
        if (spriteRenderer.sprite != null)
            return spriteRenderer.sprite.bounds.size.x;

        // Fallback: if no sprite is assigned, treat as 1 unit wide.
        return 1f;
    }

    private void ResolveCamera()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (targetCamera == null)
            Debug.LogError($"[HorizontalScreenFitter] '{name}': No camera found. " +
                           "Assign one in the Inspector or ensure a Camera tagged 'MainCamera' exists.", this);
    }

    private bool ValidateReferences()
    {
        if (targetCamera    == null) { ResolveCamera(); }
        if (spriteRenderer  == null) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        return targetCamera != null && spriteRenderer != null;
    }

    private void CacheScreenSize()
    {
        lastScreenWidth  = Screen.width;
        lastScreenHeight = Screen.height;
    }

    private bool HasScreenChanged()
    {
        return Screen.width != lastScreenWidth || Screen.height != lastScreenHeight;
    }

    // ──────────────────────────────────────────────────────────────
    // Editor Support
    // ──────────────────────────────────────────────────────────────

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Live-preview in Edit Mode when the Inspector values change.
        if (!previewInEditor || Application.isPlaying) return;

        spriteRenderer = GetComponent<SpriteRenderer>();
        ResolveCamera();

        if (targetCamera != null)
            ApplyHorizontalFit();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the screen edges this object is stretching to.
        if (targetCamera == null) return;

        float depth = GetDepthFromCamera();
        Vector3 leftEdge  = targetCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, depth));
        Vector3 rightEdge = targetCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth));

        Vector3 size = new Vector3(rightEdge.x - leftEdge.x, 0.15f, 0f);
        Gizmos.color = new Color(0f, 1f, 0.5f, 0.35f);
        Gizmos.DrawCube(new Vector3((leftEdge.x + rightEdge.x) * 0.5f, transform.position.y, transform.position.z), size);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            new Vector3(leftEdge.x,  transform.position.y - 0.2f, transform.position.z),
            new Vector3(leftEdge.x,  transform.position.y + 0.2f, transform.position.z)
        );
        Gizmos.DrawLine(
            new Vector3(rightEdge.x, transform.position.y - 0.2f, transform.position.z),
            new Vector3(rightEdge.x, transform.position.y + 0.2f, transform.position.z)
        );
    }
#endif
}