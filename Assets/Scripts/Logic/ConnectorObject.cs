using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * ConnectorObject for connection tasks
 * Supports multiple connectors used in anchorPoints[]
 */
public class ConnectorObject : InteractableObject
{
    public bool connectorActive = false;
    public GameObject preview = null;
    public GameObject[] anchorPoints;
    public Queue<GameObject> anchorQueue;

    public void Awake()
    {
        anchorQueue = new Queue<GameObject>();
        foreach (GameObject ap in anchorPoints) anchorQueue.Enqueue(ap);
    }

    public virtual void Connect(GameObject connectible)
    {
        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            connectible.transform.parent = anchorQueue.Dequeue().transform;
            connectible.GetComponent<Rigidbody>().isKinematic = true;
            foreach (Collider collider in connectible.GetComponentsInChildren<Collider>()) collider.enabled = false;
            connectible.GetComponent<InteractableObject>().SetGrabbable(false);
            connectible.transform.localPosition = new Vector3(0, 0, 0);
            connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
            connectible.GetComponent<Connectible>().SetConnected(true);
            this.connectorActive = false;
            ProcessHandler.Instance.NextTask();
            DestroyPreview();
        }
    }
    public void ForceConnect(GameObject connectible)
    {
        connectible.transform.parent = anchorQueue.Dequeue().transform;
        connectible.GetComponent<Rigidbody>().isKinematic = true;
        foreach(Rigidbody child in connectible.GetComponentsInChildren<Rigidbody>()) child.isKinematic = true;
        connectible.transform.localPosition = new Vector3(0, 0, 0);
        connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    public virtual void Disconnect() => Debug.LogError("Disconnected");
    public virtual void StartPreview(GameObject prefab)
    {
        if (preview == null)
        {
            preview = Instantiate(prefab);
            preview.GetComponent<InteractableObject>().SetGrabbable(false);
            DestroyImmediate(preview.GetComponent<XRGrabInteractable>());
            Destroy(preview.GetComponent<Rigidbody>());
            foreach (Component comp in preview.GetComponents<Component>())
            {
                if (!(comp is Transform))
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
        if (anchorQueue.Count < 1) return new Vector3(0, 0, 0);
        return anchorQueue.Peek().transform.position;
    }
}
