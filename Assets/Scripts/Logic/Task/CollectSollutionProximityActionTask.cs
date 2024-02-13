using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Abwandlung des ProximityActionTasks für PEG_MED_Collect_Sollution welcher nur auf CollectSollutionProximityActionObjects reagiert.
/// </summary>
public class CollectSollutionProximityActionTask : Task
{
    public GameObject touchObject, touchTarget;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);
        touchObject.GetComponent<CollectSollutionProximityActionObject>().SetTarget(touchTarget);
    }
    void FixedUpdate()
    {
        
    }

    protected override void CompReset()
    {
        touchObject.GetComponent<CollectSollutionProximityActionObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!touchObject.GetComponent<CollectSollutionProximityActionObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
