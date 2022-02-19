using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConnectorObject : InteractableObject
{
    public bool connectorActive = false;
    public GameObject preview = null;
    public GameObject anchorPoint = null;

    public virtual void Connect(GameObject connectible)
    {
        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            connectible.transform.parent = this.anchorPoint.transform;
            connectible.GetComponent<Rigidbody>().isKinematic = true;
            connectible.GetComponent<Collider>().enabled = false;
            connectible.GetComponent<InteractableObject>().SetGrabbable(false);
            connectible.transform.localPosition = new Vector3(0, 0, 0);
            connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
            connectible.GetComponent<Connectible>().SetConnected(true);
            ProcessHandler.Instance.NextTask();
            this.connectorActive = false;
            DestroyPreview();
            Destroy(connectible.GetComponent<Connectible>());
            Destroy(this);
        }
    }
    public void ForceConnect(GameObject connectible)
    {
        connectible.transform.parent = this.anchorPoint.transform;
        connectible.GetComponent<Rigidbody>().isKinematic = true;
        foreach(Rigidbody child in connectible.GetComponentsInChildren<Rigidbody>()) child.isKinematic = true;
        //connectible.GetComponent<Collider>().enabled = false;
        //connectible.GetComponent<InteractableObject>().SetGrabbable(false);
        connectible.transform.localPosition = new Vector3(0, 0, 0);
        connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    public virtual void Disconnect(){}
    public virtual void StartPreview(GameObject prefab)
    {
        if (preview == null)
        {
            preview = Instantiate(prefab);
            preview.GetComponent<InteractableObject>().SetGrabbable(false);
            DestroyImmediate(preview.GetComponent<XRGrabInteractable>());
            foreach (Component comp in preview.GetComponents<Component>())
            {
                if (!(comp is Transform))
                {
                    Destroy(comp);
                }
            }
            PreviewFar();
            preview.transform.parent = this.anchorPoint.transform;
            preview.transform.localPosition = new Vector3(0, 0, 0);
            preview.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void PreviewClose()
    {
        if (preview != null)
        {
            foreach (Renderer renderer in preview.GetComponentsInChildren<Renderer>())
            {
                renderer.material = ProcessHandler.Instance.GetClosePreviewMaterial();
            }
        }
    }
    public void PreviewFar()
    {
        if (preview != null)
        {
            foreach (Renderer renderer in preview.GetComponentsInChildren<Renderer>())
            {
                renderer.material = ProcessHandler.Instance.GetFarPreviewMaterial();
            }
        }
    }
    public void DestroyPreview()
    {
        if (preview != null) Destroy(preview);
    }
    public Vector3 GetAnchorPosition()
    {
        return anchorPoint.transform.position;
    }
}
