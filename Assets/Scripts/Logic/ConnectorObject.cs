using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConnectorObject : InteractableObject
{
    public bool connectorActive = false;
    [SerializeField] private GameObject anchorPoint = null;

    public void Connect(GameObject connectible)
    {
        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            connectible.transform.parent = this.anchorPoint.transform;
            connectible.GetComponent<Rigidbody>().isKinematic = true;
            connectible.GetComponent<Collider>().enabled = false;
            connectible.GetComponent<InteractableObject>().SetGrabbable(false);
            connectible.transform.localPosition = new Vector3(0, 0, 0);
            connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
            ProcessHandler.Instance.NextTask();
            Destroy(connectible.GetComponent<Connectible>());
            Destroy(this);
        }
    }
}
