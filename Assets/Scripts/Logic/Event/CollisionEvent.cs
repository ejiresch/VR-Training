using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class Representing a Collision, contains infos about the two colliders
public abstract class CollisionEvent
{
    // Reports the collision
    public void ReportCollision()
    {
        ProcessHandler.Instance.ReportCollision(this);
    }
    public abstract GameObject[] GetEventData();
}
