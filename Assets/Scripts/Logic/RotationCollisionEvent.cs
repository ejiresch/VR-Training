using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCollisionEvent : MonoBehaviour
{
    // Constructor
    public RotationCollisionEvent(GameObject socketCollider, GameObject tool, Vector3 startRotation)
    {
        SocketCollider = socketCollider;
        Tool = tool;
        StartRotation = startRotation;
    }
    public GameObject SocketCollider { get; set; }
    public GameObject Tool { get; set; }
    public Vector3 StartRotation { get; set; }
    // Reports the collision
    public void ReportCollision()
    {
        ProcessHandler.Instance.ReportRotationCollision(this);
    }
}
