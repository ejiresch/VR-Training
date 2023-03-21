using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTouchTask : Task
{
    public GameObject touchTarget;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        touchTarget = base.FindTool(touchTarget.name);
        //touchObject.GetComponent<TouchObject>().SetTouchTarget(touchTarget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
