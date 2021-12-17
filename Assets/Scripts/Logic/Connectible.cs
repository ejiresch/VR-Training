using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectible : InteractableObject
{
    private ConnectorObject connector;
    public float distance = 0;
    private float range = 0.15f;

    public override void OnDrop()
    {
        base.OnDrop();
        if (connector != null)
        {
            if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < range) connector.Connect(this.gameObject);
        }
    }
    public override void SetIsGrabbed(bool isg)
    {
        base.SetIsGrabbed(isg);
        if (connector != null)
        {
            if (isg)
            {
                StopCoroutine(CheckDistance());
                StartCoroutine(CheckDistance());
            }
            else connector.DestroyPreview();
        }
    }
    public void SetConnector(ConnectorObject connector)
    {
        this.connector = connector;
        if (GetIsGrabbed() && connector != null) StartCoroutine(CheckDistance());
    }
    IEnumerator CheckDistance()
    {
        for(; GetIsGrabbed();)
        {
            if (connector != null)
            {
                if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < range)
                {
                    distance = (connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude;
                    connector.Preview(this.gameObject);
                }
                else
                {
                    connector.DestroyPreview();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
