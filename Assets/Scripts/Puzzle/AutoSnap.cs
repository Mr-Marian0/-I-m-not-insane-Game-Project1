using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutoSnap : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(0.32f, 0.50f);

    void Update()
    {
        Vector3 localPos = transform.localPosition;
        localPos.x = Mathf.Round(localPos.x / gridSize.x) * gridSize.x;
        localPos.y = Mathf.Round(localPos.y / gridSize.y) * gridSize.y;
        transform.localPosition = localPos;
    }
}