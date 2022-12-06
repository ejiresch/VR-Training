using System;
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
    // Order of the AnchorPoints
    public Queue<GameObject> anchorQueue;
    protected Transform previousParent;
    public bool resetOnTaskCompletion = false;
    // Wird am Anfang ausgefuehrt
    public void Awake()
    {
        anchorQueue = new Queue<GameObject>();
        foreach (GameObject ap in anchorPoints) anchorQueue.Enqueue(ap);
    }
    // Verbindet ein Connectible mit dem Objekt am definiertem Anchorpoint
    public virtual void Connect(GameObject connectible)
    {

        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            previousParent = connectible.transform.parent;
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
    // Verbindet wiederum ein Connectible mit dem ConnectorObject, ohne auf Bedingungen zu achten
    public void ForceConnect(GameObject connectible)
    {
        connectible.transform.parent = anchorQueue.Dequeue().transform;
        connectible.GetComponent<Rigidbody>().isKinematic = true;
        foreach(Rigidbody child in connectible.GetComponentsInChildren<Rigidbody>()) child.isKinematic = true;
        connectible.transform.localPosition = new Vector3(0, 0, 0);
        connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    // Kann implementiert werden um Objekte wieder zu entfernen
    public virtual void Disconnect() {
        GameObject anchorPoint = anchorQueue.Peek();
        if (anchorPoint.transform.childCount > 0)
        {
            GameObject go = anchorPoint.transform.gameObject;
            if (go)
            {
                try
                {
                    ResetManager resetm;
                    if (resetm = go.GetComponent<ResetManager>())
                    {
                        resetm.ResetComp();
                    }
                    go.transform.parent = previousParent;
                    go.GetComponent<Rigidbody>().isKinematic = false;
                    go.GetComponentInChildren<BoxCollider>().enabled = true;
                    go.GetComponent<XRBaseInteractable>().interactionLayerMask = ~0;
                }
                catch (Exception e)
                {
                    Debug.LogError("" + e.Source);
                }

            }
        }
    }
    // Startet den Preview (Rote Vorzeige)
    public virtual void StartPreview(GameObject prefab)
    {
        if (preview == null)
        {
            preview = Instantiate(prefab);
            preview.GetComponent<InteractableObject>().SetGrabbable(false);
            preview.GetComponent<Rigidbody>().isKinematic = true;
            foreach (Component comp in preview.GetComponents<Component>())
            {
                // Rigidbody must not be destroyed, else crashes
                if (!(comp is Transform) && !(comp is Rigidbody))
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
    // Aendert die Farbe des Previews auf gruen
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
    // Aendert die Farbe des Previews auf rot
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
    // Zerstoert das Preview
    public void DestroyPreview()
    {
        if (preview != null) Destroy(preview);
    }
    // Holt sich den naechsten Anchorpoint
    public Vector3 GetAnchorPosition()
    {
        if (anchorQueue.Count < 1) return new Vector3(0, 0, 0);
        return anchorQueue.Peek().transform.position;
    }
}
