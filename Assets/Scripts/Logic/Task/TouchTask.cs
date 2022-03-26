using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTask : Task
{
    public GameObject touchObject, touchTarget;
    public override void StartTask()
    {
        base.StartTask();
        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);
        touchObject.GetComponent<TouchObject>().SetTouchTarget(touchTarget);
    }
}
