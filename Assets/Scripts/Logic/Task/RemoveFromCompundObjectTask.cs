using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Task welche ein CompoundPart von einem CompoundObject entfernen soll
// Somit wird das beim Auseinanderbauen verwendet
public class RemoveFromCompundObjectTask : Task
{
    [SerializeField] private GameObject compoundObject;
    private GameObject objectToRemove;
    // Wird beim Start der Task ausgefuehrt
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
            compP.SetTaskFocus(true);
        }
    }

    protected override void CompReset()
    {
        objectToRemove.GetComponent<CompundPart>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (objectToRemove == null)
        {
            yield return new WaitForFixedUpdate();
        }
        while (!objectToRemove.GetComponent<CompundPart>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
