using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine Task, welche verwendet wird bei Objekten welche betaetigt werden muessen
public class PressTask : Task
{
    public GameObject pressObject;
    // Wird beim Start der Task aufgerufen
    public override void StartTask()
    {
        base.StartTask();
        pressObject = base.FindTool(pressObject.name);
        pressObject.GetComponent<PressObject>().SetPressable(true);
    }
}
