using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Does not support multiple anchorPoints !!!
 */
public class CuffllineConnectionObject : ConnectorObject
{
    private Transform previousParent;
    public override void Connect(GameObject connectible)
    {
        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            previousParent = connectible.transform.parent;
            Debug.Log(previousParent.name);
            connectible.transform.parent = anchorQueue.Peek().transform;
            connectible.GetComponent<Rigidbody>().isKinematic = true;
            connectible.GetComponent<Collider>().enabled = false;
            connectible.GetComponent<InteractableObject>().SetGrabbable(false);
            connectible.transform.localPosition = new Vector3(0, 0, 0);
            connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
            connectible.GetComponent<Connectible>().SetConnected(true);
            this.connectorActive = false;
            DestroyPreview();
            GetComponent<PressObject>().SetPressable(true);
        }

    }
    public override void Disconnect()
    {
        base.Disconnect();
        GameObject anchorPoint = anchorQueue.Peek();
        if (anchorPoint.transform.childCount > 0)
        {
            GameObject child = anchorPoint.transform.GetChild(0).gameObject;
            if (child)
            {
                child.transform.parent = previousParent;
                child.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    public override void StartPreview(GameObject prefab)
    {
        if (preview == null)
        {
            preview = Instantiate(prefab);
            preview.GetComponent<InteractableObject>().SetGrabbable(false);
            preview.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(preview.GetComponent<Joint>());
            foreach (Component comp in preview.GetComponents<Component>())
            {
                if (!(comp is Transform) && !(comp is MeshRenderer) && !(comp is MeshFilter) && !(comp is Rigidbody))
                {
                    Destroy(comp);
                }
            }
            foreach (Collider collider in preview.GetComponentsInChildren<Collider>()) Destroy(collider);
            PreviewFar();
            preview.transform.parent = anchorQueue.Peek().transform;
            preview.transform.localPosition = new Vector3(0, 0, 0);
            preview.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
