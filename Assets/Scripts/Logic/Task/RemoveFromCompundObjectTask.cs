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
        objectToRemove = compoundObject.GetComponent<CompundObject>().GetPart();
        if (objectToRemove == null)
        {
            Debug.LogError("Fix your task list");
            return;
        }
        objectToRemove.GetComponent<InteractableObject>().SetGrabbable(true);
    }
}
