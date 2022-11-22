using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Object that can be connected to a ConnectorObject
public class Connectible : InteractableObject
{
    private ConnectorObject connector = null;
    private bool inReach = false;
    private bool connected = false;
    private float range = 0.15f;
    private int numGrabbed = 0;
    Dictionary<string, Color> originalColors = new Dictionary<string, Color>();

    // Falls das Objekt losgelassen wird, wird diese Methode ausgefuehrt
    public override void OnDrop()
    {
        base.OnDrop();
        if (connector != null)
        {
            if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < range)
            {
                connector.Connect(this.gameObject);
            }
        }
    }
    // Wird ausgefuehrt wenn das Objekt gehoben oder 
    public override void SetIsGrabbed(bool isg)
    {
        base.SetIsGrabbed(isg);
        if (connector != null)
        {
            if (isg)
            {
                StopCoroutine(CheckDistance());
                StartCoroutine(CheckDistance());
                connector.StartPreview(this.gameObject);
                numGrabbed++;
            }
            else
            {
                numGrabbed--;
                if (numGrabbed == 0)
                {
                    StopCoroutine(CheckDistance());
                    connector.DestroyPreview();
                }
            }
        }
    }
    public void SetConnector(ConnectorObject connector)
    {
        this.connector = connector;
        this.SetGrabbable(true);
        if (GetIsGrabbed() && connector != null)
        {
            connector.StartPreview(this.gameObject);
            StartCoroutine(CheckDistance());
        }
    }
    public void SetConnected(bool connected)
    {
        this.connected = connected;
        StopAllCoroutines();
    }
    // On each Frame check if the Connectible Object is in Range of the ConnectorObject
    IEnumerator CheckDistance()
    {
        
        for(; GetIsGrabbed();)
        {
            if (connector != null)
            {
                if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < range)
                {
                    if (!inReach)
                    {
                        connector.PreviewClose();
                        inReach = true;
                    }
                }
                else
                {
                    if (inReach)
                    {
                        connector.PreviewFar();
                        inReach = false;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void OutOfReachColor()
    {
        if (!connected)
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = new Color(0.7f, 0f, 0f);
            }
        }
    }
    public void InReachColor()
    {
        if (!connected)
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = new Color(0f, 0.7f, 0f);
            }
        }
    }
    public void RestoreOriginalColor()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = originalColors[renderer.name];
        }
    }
}
