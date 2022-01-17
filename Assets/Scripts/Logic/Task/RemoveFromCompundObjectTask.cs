using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFromCompundObjectTask : Task
{
    [SerializeField] private GameObject compoundObject;
    private GameObject objectToRemove;
    // Start is called before the first frame update
    public override void StartTask()
    {
        base.StartTask();
        CompundObject cO = ProcessHandler.Instance.GetCompoundObject().GetComponent<CompundObject>();
        if (cO) compoundObject = cO.gameObject;
        objectToRemove = compoundObject.GetComponent<CompundObject>().GetPart();
        if (objectToRemove)
        {
            CompundPart compP = objectToRemove.GetComponent<CompundPart>();
            compP.SetGrabbable(true);
            compP.SetIsGrabbed(true);
            compP.SetTaskFocus(true);
        }
        else
        {
            Debug.LogError("Fix your task list");
            Debug.Log(objectToRemove);
            return;
        }
    }
}
