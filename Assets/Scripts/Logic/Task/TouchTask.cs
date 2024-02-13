using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Task, welche bei Beruehrung zweier Objekte beendet wird
public class TouchTask : Task
{
    public GameObject touchObject, touchTarget;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);
        touchObject.GetComponent<TouchObject>().SetTouchTarget(touchTarget);
        base.StartTask();
    }

    protected override void CompReset()
    {
        touchObject.GetComponent<TouchObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!touchObject.GetComponent<TouchObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
