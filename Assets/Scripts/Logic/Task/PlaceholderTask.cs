using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Placeholder Task, lediglich fuer Placeholder sinnvoll
// Wird bei Tastendruck beendet
public class PlaceholderTask : Task
{
    public InputActionReference toggleReferenceRight = null;
    private bool taskOk = false;
    public override void StartTask()
    {
        base.StartTask();
    }

    protected override void CompReset()
    {
        
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!taskOk)
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
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
        taskOk = true;
    }

}
