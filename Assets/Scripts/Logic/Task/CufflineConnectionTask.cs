using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CufflineConnectionTask : Task
{
    public GameObject connector;
    public override void StartTask()
    {
        base.StartTask();
        connector = base.FindTool(connector.name);
        GameObject connectible = GameObject.FindGameObjectWithTag("CompoundGrabbablePart");
        if (connector != null && connectible != null)
        {
            connector.GetComponent<ConnectorObject>().connectorActive = true;
            connectible.GetComponent<Connectible>().SetConnector(connector.GetComponent<ConnectorObject>());
        }
    }
}