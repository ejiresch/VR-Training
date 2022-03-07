using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpritzePressObject : PressObject
{
    public InputActionReference toggleReferenceLeft = null;
    public InputActionReference toggleReferenceRight = null;
    public bool reingepumpt = false;
    public GameObject kolben;

    private void Awake()
    {
        toggleReferenceLeft.action.started += Toggle;
        toggleReferenceRight.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReferenceLeft.action.started -= Toggle;
        toggleReferenceRight.action.started -= Toggle;
    }
    private void Toggle(InputAction.CallbackContext context)
    {
        if (!reingepumpt)
        {
            //  hier wird reingepumpt
            reingepumpt = true;
            StartCoroutine(Reinpumpen());
        }
        else
        {
            //  hier wird rausgepumpt und task beendet
            StartCoroutine(Rauspumpen());
            Press();
            GetComponent<ConnectorObject>().Disconnect();
            GameObject temp = GameObject.FindGameObjectWithTag("CompoundGrabbablePart").transform.parent.parent.gameObject;
            temp.GetComponent<InteractableObject>().SetGrabbable(true);
            temp.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    IEnumerator Reinpumpen()
    {

        if (this.GetComponent<InteractableObject>().GetIsGrabbed())
        {
            for (int i = 0; i < 50; i++)
            {
                kolben.transform.localPosition -= new Vector3(0, 0.05f, 0);
                if (kolben.transform.localPosition.y < 6) { break; }
                yield return new WaitForSeconds(0.008f);
            }
            reingepumpt = true;
        }
    }
    IEnumerator Rauspumpen()
    {
        if (this.GetComponent<InteractableObject>().GetIsGrabbed())
        {
            for (int i = 0; i < 50; i++)
            {
                kolben.transform.position += new Vector3(0, 0.05f, 0);
                if (kolben.transform.position.y > 9) { break; }
                yield return new WaitForSeconds(0.008f);
            }
            Press();
            GetComponent<ConnectorObject>().Disconnect();
        }
    }
}
