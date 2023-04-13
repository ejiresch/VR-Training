using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPressTask : PressTask
{
    public float distance = 1.0f;
    private bool active = false;
    GameObject[] ProxObjects = new GameObject[2];
    private bool taskactive;
    // Start is called before the first frame update
    void Start()
    {   
        active = true;
    }
    public override void StartTask()
    {
        base.StartTask();
        taskactive = true;
        
    }
        // Update is called once per frame
    void Update()
    {
        if (active)
        {

            GameObject[] touchObjects = GameObject.FindGameObjectsWithTag("touchHand");
            ProxObjects = touchObjects;
            if (touchObjects != null && touchObjects.Length > 1)
            {
                Debug.Log(touchObjects.Length);
                foreach (GameObject touchObject in touchObjects)
                {
                    touchObject.GetComponent<ProximityActionObject>().SetTouchTarget(pressObject);
                }
                active = false;
            }
        }
        pressObject.GetComponent<InteractableHandler>().SetIsGrabbed(Vector3.Distance(pressObject.transform.position, this.transform.position) < distance);
    }
    void FixedUpdate()
    {
        if (!active && taskactive)
        {
            pressObject.GetComponent<PressObject>().SetIsGrabbed(ProxObjects[0].GetComponent<ProximityActionObject>().GetInRange() || ProxObjects[1].GetComponent<ProximityActionObject>().GetInRange());
            if (pressObject.GetComponent<PressObject>().GetTaskCompletion())
            {
                pressObject.GetComponent<PressObject>().SetTaskFinished(false);
                pressObject.GetComponent<PressObject>().SetIsGrabbed(false);
                EndTask();
                taskactive = false;
            }
        }
    }
    protected override void EndTask()
    {
        base.EndTask();
    }
}
