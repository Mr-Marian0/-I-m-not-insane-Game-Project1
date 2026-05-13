using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FillParentWithPadding : MonoBehaviour
{

    // THIS SCRIPT IS USED TO FIT SCREEN ACCORDING TO "UI" GAME OBJECT

    [SerializeField] private float extraScale = 1.05f;

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();

        // Stretch to parent
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;

        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Slightly enlarge to hide edge gaps
        rt.localScale = new Vector3(extraScale, extraScale, 1f);
    }
}