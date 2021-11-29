using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCollisionEvent : CollisionEvent
{
    public GameObject SocketCollider { get; set; }
    public GameObject Tool { get; set; }
    public Vector3 StartRotation { get; set; }
    // Constructor
    public RotationCollisionEvent(GameObject socketCollider, GameObject tool, Vector3 startRotation)
    {
        SocketCollider = socketCollider;
        Tool = tool;
        StartRotation = startRotation;
    }

    public override GameObject[] GetEventData()
    {
        GameObject[] daten = { SocketCollider, Tool };
        return daten;
    }
    public Vector3 GetRotation()
    {
        return StartRotation;
    }
}
