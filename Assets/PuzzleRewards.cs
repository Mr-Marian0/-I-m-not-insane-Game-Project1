using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRewards : MonoBehaviour
{

    public Vector3 TrustDefaultPosition;
    public Vector3 StressDefualtPosition;
    public RectTransform MoveTrustPosition;
    public RectTransform MoveStressPosition;

    public void OnDisable() 
    {
    if (MoveTrustPosition != null) MoveTrustPosition.anchoredPosition = TrustDefaultPosition;
    if (MoveStressPosition != null) MoveStressPosition.anchoredPosition = StressDefualtPosition;
    }

}
