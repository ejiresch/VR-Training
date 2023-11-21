using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HandTouchTask : Task
{
    public GameObject touchTarget;
    private bool active = false;
    private GameObject[] touchObjects;
    // Wird bei Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        
        touchTarget = base.FindTool(touchTarget.name);
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

            touchObjects = GameObject.FindGameObjectsWithTag("Hand");
            if (touchObjects != null && touchObjects.Length>1)
            {
                Debug.Log(touchObjects.Length);
                foreach (GameObject touchObject in touchObjects)
                {
                    touchObject.GetComponent<TouchHand>().SetTouchTarget(touchTarget);
                    touchObject.GetComponent<TouchHand>().SetHands(new List<GameObject>() { touchObjects[0], touchObjects[1] });
                }
                active = false;
            }
        }
    }

    //Dazu, Simon
    public override List<GameObject> HighlightedObjects()
    {
        List<GameObject> result = new List<GameObject>();
        //result.Add(touchObject);
        result.Add(touchTarget);
        return result;
    }

    protected override void CompReset()
    {
        touchObjects[0].GetComponent<TouchHand>().SetTaskFinished(false);
        touchObjects[1].GetComponent<TouchHand>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (touchObjects == null)
        {

            yield return new WaitForFixedUpdate();
        }
        while (!touchObjects[0].GetComponent<TouchHand>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}
