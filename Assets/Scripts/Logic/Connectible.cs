using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectible : InteractableObject
{
    public ConnectorObject connector;

    public override void OnDrop()
    {
        base.OnDrop();
        Debug.Log(connector.gameObject.transform.position.magnitude);
        Debug.Log((this.gameObject.transform.position - connector.gameObject.transform.position).magnitude);
        if ((this.gameObject.transform.position - connector.gameObject.transform.position).magnitude < 0.2) connector.Connect(this.gameObject);
    }
}
