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
    public bool hasToBeGrabbed = true;
    public bool connectorActive = false;
    public GameObject preview = null;
    public GameObject[] anchorPoints;
    // Order of the AnchorPoints
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

        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && (this.GetIsGrabbed()||!hasToBeGrabbed))
        {
            aStore.StoreObj(connectible);
            connectible.GetComponent<Connectible>().ResetOnDropFunc();
            connectible.GetComponent<Rigidbody>().isKinematic = true;
            // Alle Collider werden deaktiviert abgesehen von Collidern die auf Objekten des Layers "NoColliderOff" sind. 
            foreach (Collider collider in connectible.GetComponentsInChildren<Collider>())
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("NoColliderOff")) continue;
                collider.enabled = false;
            }
            connectible.GetComponent<InteractableObject>().SetGrabbable(false);
            connectible.transform.localPosition = new Vector3(0, 0, 0);
            connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
            connectible.GetComponent<Connectible>().SetConnected(true);
            taskfinished = true;
            this.connectorActive = false;
            DestroyPreview();
        }
    }
    // Verbindet wiederum ein Connectible mit dem ConnectorObject, ohne auf Bedingungen zu achten
    public void ForceConnect(GameObject connectible)
    {
        aStore.StoreObj(connectible);
        connectible.GetComponent<Rigidbody>().isKinematic = true;
        foreach(Rigidbody child in connectible.GetComponentsInChildren<Rigidbody>()) child.isKinematic = true;
        connectible.transform.localPosition = new Vector3(0, 0, 0);
        connectible.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    // Kann implementiert werden um Objekte wieder zu entfernen
    public virtual void Disconnect() {
        GameObject anchorPoint = aStore.GetLatestConnectedObject();
        if (anchorPoint.transform.childCount > 0)
        {
            GameObject go = anchorPoint.transform.gameObject;
            if (go)
            {
                try
                {
                    Connectible connectible = anchorPoint.GetComponent<Connectible>();
                    connectible.SetOnDropFunc((x)=>{ return x.GetComponent<Rigidbody>().isKinematic = false; });
                    connectible.GetComponent<Connectible>().SetOnDropFunc((x)=>{ return x.transform.parent = ProcessHandler.Instance.transform; });
                    go.transform.parent = aStore.GetLatestConnectedParent();
                    go.GetComponent<Rigidbody>().isKinematic = false;
                    foreach (Collider collider in go.GetComponentsInChildren<Collider>()) collider.enabled = true;
                    go.GetComponent<XRBaseInteractable>().interactionLayers = InteractionLayerMask.GetMask("Default");
                    taskfinished = true;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

            }
        }
        aStore.RemoveObj();
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
            preview.transform.parent = aStore.NextFreeAnchor();    
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
        return aStore.NextFreeAnchorPosition();
    }


    /// <summary>
    /// Stellt Methoden zum Speichern von AnchorPoints und mit diesen verbundenen Objekten bereit
    /// </summary>
    protected class AnchorStore
    {
        private Transform[] parents;
        private GameObject[] go;
        private readonly Transform[] anchors;
        private int index = 0;

        /// <summary>
        /// Erzeugt einen AnchorStore mit AnchorPoints die mit Objekten verbunden werden können 
        /// </summary>
        /// <param name="anchorObjects"> Die AnchorPoints die zum speichern eines Objektes verfügbar sein sollen</param>
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
        /// <summary>
        /// Speichert ein Objekt im AnchorStore, dabei wird das Parent Object gespeichert und anschließend der nächste freie Anchor als neuer Parent gesetzt.
        /// </summary>
        /// <param name="obj">Das Objekt, das gespeichert werden soll.</param>
        internal void StoreObj(GameObject obj)
        {
            if (index < anchors.Length)
            {
                go[index] = obj;
                parents[index] = obj.transform.parent;
                obj.transform.parent = anchors[index];
                index++;
            }
        }
        /// <summary>
        /// Gibt das letzte GameObject zurück, das im AnchorStore gespeichert wurde.
        /// </summary>
        /// <returns>Das GameObject, das als letztes gespeichert wurde</returns>
        public GameObject GetLatestConnectedObject() => index>0 ? go[index - 1] : null;
        /// <summary>
        /// Gibt das Parent Objekt des zuletzt gespeicherten GameObjects zurück.
        /// </summary>
        /// <returns>Parent des zuletzt gespeicherten GameObject</returns>
        public Transform GetLatestConnectedParent() => index>0 ? parents[index - 1] : null;
        /// <summary>
        /// Entfernt das zuletzt hinzugefügte GameObject aus dem AnchorStore.
        /// Dabei wird das Parent Objekt auf das zuvor gespeicherte Parent Objekt gesetzt.
        /// </summary>
        public void RemoveObj()
        {
            if (index > 0)
            {
                go[index-1].transform.parent = parents[index-1].transform;
                index--;
            }
        }
        /// <summary>
        /// Durchsucht den AnchorStore nach dem nächsten freien Anchor und gibt die Position zurück.
        /// </summary>
        /// <returns>Position des nächsten freien Anchor in <b>Vector3</b></returns>
        public Vector3 NextFreeAnchorPosition()
        {
            if (index == anchors.Length) return new Vector3(0, 0, 0);
            return anchors[index].position;
        }
        /// <summary>
        /// Gibt den nächsten freien Anchor zurück.
        /// </summary>
        /// <returns>Das Transform des nächsten Anchors. Wenn es keinen freien gibt, wird der letzte in der liste zurückgegeben.</returns>
        public Transform NextFreeAnchor()
        {
            if (index == anchors.Length) return anchors[anchors.Length-1];
            return anchors[index];
        }
    }
}
