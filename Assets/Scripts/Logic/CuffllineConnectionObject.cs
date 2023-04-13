using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Stellt eine Alternative zum ConnectorObject dar und wird bei der Verbindung mit der Cuffline verwendet
 * Does not support multiple anchorPoints !!!
 */
public class CuffllineConnectionObject : ConnectorObject
{
    // Verbindet ein Connectible mit dem Objekt am definiertem Anchorpoint
    public override void Connect(GameObject connectible)
    {
        if (!connectible.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive && this.GetIsGrabbed())
        {
            aStore.StoreObj(connectible);
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
    // Entfernt angehaengte Objekte
    public override void Disconnect()
    {
        //base.Disconnect();
        GameObject anchorPoint = aStore.GetLatestConnectedObject();
        if (anchorPoint.transform.childCount > 0)
        {
            GameObject child = anchorPoint.transform.GetChild(0).gameObject;
            if (child)
            {
                child.transform.parent = aStore.GetLatestConnectedParent();
                Debug.LogWarning(child.transform.parent);
                child.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    // Startet den Preview (Rote Vorzeige)
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
            preview.transform.parent = aStore.GetLatestConnectedParent();
            preview.transform.localPosition = new Vector3(0, 0, 0);
            preview.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
