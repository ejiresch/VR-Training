using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTask : Task
{
    public GameObject pressObject;
    public override void StartTask()
    {
        base.StartTask();
        pressObject = base.FindTool(pressObject.name);
        pressObject.GetComponent<PressObject>().SetPressable(true);
    }
}
