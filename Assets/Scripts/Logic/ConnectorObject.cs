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
    [Tooltip("Setzt das Component nach der Reset-Methode zurück")]
    public bool resetOnTaskCompletion = false;
    protected Transform previousParent;
    protected AnchorStore aStore;
    // Wird am Anfang ausgefuehrt
    public void Awake()
    {
        aStore = new AnchorStore(anchorPoints);
    }
    // Verbindet ein Connectible mit dem Objekt am definiertem Anchorpoint
    public virtual void Connect(GameObject connectible)
    {

        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            aStore.storeOb(connectible);
            connectible.GetComponent<Connectible>().ResetOnDropFunc();
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
        aStore.storeOb(connectible);
        connectible.GetComponent<Rigidbody>().isKinematic = true;
        foreach(Rigidbody child in connectible.GetComponentsInChildren<Rigidbody>()) child.isKinematic = true;
        connectible.transform.localPosition = new Vector3(0, 0, 0);
        connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    // Kann implementiert werden um Objekte wieder zu entfernen
    public virtual void Disconnect() {
        GameObject anchorPoint = aStore.getLatestConnectedObject();
        if (anchorPoint.transform.childCount > 0)
        {
            GameObject go = anchorPoint.transform.gameObject;
            if (go)
            {
                try
                {
                    anchorPoint.GetComponent<Connectible>().SetOnDropFunc((x)=>{ return x.GetComponent<Rigidbody>().isKinematic = false; });
                    anchorPoint.GetComponent<Connectible>().SetOnDropFunc((x)=>{ return x.transform.parent = ProcessHandler.Instance.transform; });
                    go.transform.parent = aStore.getLatestConnectedParent();
                    go.GetComponent<Rigidbody>().isKinematic = false;
                    foreach (Collider collider in go.GetComponentsInChildren<Collider>()) collider.enabled = true;
                    go.GetComponent<XRBaseInteractable>().interactionLayerMask = ~0;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

            }
        }
        aStore.removeObj();
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
            preview.transform.parent = aStore.nextFreeAnchor();    
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
        return aStore.nextFreeAnchorPosition();
    }



    protected class AnchorStore
    {
        private Transform[] parents;
        private GameObject[] go;
        private readonly Transform[] anchors;
        private int index = 0;

        public AnchorStore(GameObject[] anchorObjects)
        {
            anchors = new Transform[anchorObjects.Length];
            parents = new Transform[anchorObjects.Length];
            go = new GameObject[anchorObjects.Length];
            
            for (int i = 0; i < anchorObjects.Length; i++)
            {
                anchors[i] = anchorObjects[i].transform;
            }
        }
        internal void storeOb(GameObject obj)
        {
            if (index < anchors.Length)
            {
                go[index] = obj;
                parents[index] = obj.transform.parent;
                obj.transform.parent = anchors[index];
                index++;
            }
        }
        public GameObject getLatestConnectedObject() => index>0 ? go[index - 1] : null;
        public Transform getLatestConnectedParent() => index>0 ? parents[index - 1] : null;
        public void removeObj()
        {
            if (index > 0)
            {
                go[index-1].transform.parent = parents[index-1].transform;
                index--;
            }
        }
        public Vector3 nextFreeAnchorPosition()
        {
            if (index == anchors.Length) return new Vector3(0, 0, 0);
            return anchors[index].position;
        }
        public Transform nextFreeAnchor()
        {
            if (index == anchors.Length) return anchors[anchors.Length-1];
            return anchors[index];
        }
    }
}
