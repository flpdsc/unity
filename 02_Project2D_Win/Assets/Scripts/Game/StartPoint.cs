using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : Singleton<StartPoint>
{
    [SerializeField] Transform startPivot;

    public void SetStartPoint(Transform target)
    {
        target.position = startPivot.position;
    }
}