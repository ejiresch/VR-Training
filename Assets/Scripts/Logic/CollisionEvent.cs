using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class Representing a Collision, contains infos about the two colliders
public class CollisionEvent
{
    // Constructor
    public CollisionEvent(GameObject first, GameObject second)
    {
        First = first;
        Second = second;
    }
    public GameObject First { get; set; }
    public GameObject Second { get; set; }
    // Reports the collision
    public void ReportCollision()
    {
        ProcessHandler.Instance.ReportCollision(this);
    }
}
