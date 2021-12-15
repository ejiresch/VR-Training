using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectible : InteractableObject
{
    public ConnectorObject connector;
    public float distance = 0;

    public override void OnDrop()
    {
        base.OnDrop();
        if (connector != null)
        {
            if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < 0.1f) connector.Connect(this.gameObject);
        }
    }
    public override void SetIsGrabbed(bool isg)
    {
        base.SetIsGrabbed(isg);
        if (connector != null)
        {
            if (isg) StartCoroutine(CheckDistance());
            else connector.DestroyPreview();
        }
    }
    IEnumerator CheckDistance()
    {
        for(; GetIsGrabbed();)
        {
            if (connector != null)
            {
                if ((connector.GetAnchorPosition() - this.gameObject.transform.position).magnitude < 0.1f)
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
