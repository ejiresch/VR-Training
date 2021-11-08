using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTask : Task
{
    public GameObject tool, targetCollider;
    public Vector3 targetRotation;

    // Checks the success condition
    public override bool IsSuccessful(RotationCollisionEvent ce)
    {
        Debug.Log(ce.SocketCollider + " + ++ ++" + ce.StartRotation + " + ++ + ++ + " + ce.Tool);
        return false;
    }
}

