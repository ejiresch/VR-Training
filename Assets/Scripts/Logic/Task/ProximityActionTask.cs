using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Task, welche bei Beruehrung zweier Objekte beendet wird
/// <summary>
/// Dieser Task überprüft, ob das übergebene TouchObject (in diesem Fall muss es ein ProximityActionObject sein) 
/// Die festgelegte Bedingung erfüllt.<br> Wird die Bedingung erfüllt so wird der nächste Task durch die OnDestroy Methode eingeleitet.</br>
/// </summary>
/// <remarks><b>Autor:</b> Marvin Fornezzi </remarks>
public class ProximityActionTask : Task
{
    public GameObject touchObject, touchTarget;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);
        touchObject.GetComponent<ProximityActionObject>().SetTarget(touchTarget);
    }
    void FixedUpdate()
    {
        
    }

    protected override void CompReset()
    {
        touchObject.GetComponent<ProximityActionObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!touchObject.GetComponent<ProximityActionObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
