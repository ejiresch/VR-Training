using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConnectorObject : InteractableObject
{
    private GameObject connectee;
    private bool connectorActive = false;
    [SerializeField] private GameObject anchorPoint = null;

    private void OnCollisionExit(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.layer == 9)
        {
            if (!otherObject.GetComponent<InteractableObject>().GetIsGrabbed() && connectorActive)
            {
                if (otherObject.name != string.Format("{0}(Clone)", connectee.name)) return;
                otherObject.transform.parent = this.anchorPoint.transform;
                otherObject.GetComponent<Rigidbody>().isKinematic = true;
                otherObject.GetComponent<Rigidbody>().useGravity = false;
                otherObject.GetComponent<SphereCollider>().enabled = false;
                otherObject.GetComponent<InteractableObject>().SetGrabbable(false);
                otherObject.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
