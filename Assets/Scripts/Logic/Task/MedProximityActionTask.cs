using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Task, welcher bei Beruehrung zweier Objekte beendet wird

public class MedProximityActionTask : Task
{
    public GameObject touchObject, touchTarget;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);
        touchObject.GetComponent<MedProximityActionObject>().SetTarget(touchTarget);
    }
    void FixedUpdate()
    {
        
    }

    protected override void CompReset()
    {
        touchObject.GetComponent<MedProximityActionObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!touchObject.GetComponent<MedProximityActionObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
