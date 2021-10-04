using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent
{
    public CollisionEvent(GameObject first, GameObject second)
    {
        First = first;
        Second = second;
    }
    public GameObject First { get; set; }
    public GameObject Second { get; set; }

    public void ReportCollision()
    {
        ProcessHandler.Instance.ReportCollision(this);
    }
}
