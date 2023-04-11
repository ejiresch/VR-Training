using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Task, welche bei Beruehrung zweier Objekte beendet wird
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
        if (touchObject.GetComponent<ProximityActionObject>().GetTaskCompletion())
        {
            Destroy(this);
        }
    }
    //Dazu, Simon
    public override List<GameObject> HighlightedObjects()
    {
        List<GameObject> result = new List<GameObject>();
        result.Add(touchTarget);
        result.Add(touchObject);
        return result;
    }
    void OnDestroy()
    {
        touchObject.GetComponent<ProximityActionObject>().SetTaskFinished(false);
        ProcessHandler.Instance.NextTask();
    }
}
