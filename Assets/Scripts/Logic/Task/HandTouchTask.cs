using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTouchTask : Task
{
    public GameObject touchTarget;
    private bool active = false;
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
            GameObject[] touchObjects = GameObject.FindGameObjectsWithTag("touchHand");
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
}
