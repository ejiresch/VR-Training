using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderTask : Task
{
    public InputActionReference toggleReferenceRight = null;
    public override void StartTask()
    {
        base.StartTask();
    }
    private void Awake()
    {
        toggleReferenceRight.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReferenceRight.action.started -= Toggle;
    }
    private void Toggle(InputAction.CallbackContext context)
    {
        ProcessHandler.Instance.NextTask();
    }

}
