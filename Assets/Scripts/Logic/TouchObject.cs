using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : InteractableObject
{
    private GameObject touchTarget;
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.Equals(touchTarget))
        {
            ProcessHandler.Instance.NextTask();
            touchTarget = null;
        }
    }
    public void SetTouchTarget(GameObject touchTarger) => touchTarget = touchTarger;
}
