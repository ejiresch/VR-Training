using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectible : InteractableObject
{
    private ConnectorObject connector;
    private bool inReach = false;
    private bool connected = false;
    private float range = 0.15f;
    Dictionary<string, Color> originalColors = new Dictionary<string, Color>();

    public override void OnDrop()
    {
        base.OnDrop();
        if (connector != null)
        {
            if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < range)
            {
                RestoreOriginalColor();
                connector.Connect(this.gameObject);
            }
        }
    }
    public override void SetIsGrabbed(bool isg)
    {
        base.SetIsGrabbed(isg);
        if (connector != null)
        {
            if (isg)
            {
                OutOfReachColor();
                StopCoroutine(CheckDistance());
                StartCoroutine(CheckDistance());
            }
            else
            {
                connector.DestroyPreview();
                RestoreOriginalColor();
            }
        }
    }
    public void SetConnector(ConnectorObject connector)
    {
        this.connector = connector;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            if(!originalColors.ContainsKey(renderer.name)) originalColors.Add(renderer.name, renderer.material.color);
        }

        if (GetIsGrabbed() && connector != null) StartCoroutine(CheckDistance());
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
    public void SetConnected(bool connected)
    {
        this.connected = connected;
        if (connected) RestoreOriginalColor();
    }
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
                        connector.Preview(this.gameObject);
                        InReachColor();
                        inReach = true;
                    }
                }
                else
                {
                    if (inReach)
                    {
                        connector.DestroyPreview();
                        OutOfReachColor();
                        inReach = false;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
