using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Abwandlung des ProximityActionTasks für PEG_MED_Solve welcher nur auf SolveProximityActionObjects reagiert.
/// </summary>
public class SolveProximityActionTask : Task
{
    public GameObject touchObject, touchTarget;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);
        touchObject.GetComponent<SolveProximityActionObject>().SetTarget(touchTarget);
    }
    void FixedUpdate()
    {
        
    }

    protected override void CompReset()
    {
        touchObject.GetComponent<SolveProximityActionObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!touchObject.GetComponent<SolveProximityActionObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
