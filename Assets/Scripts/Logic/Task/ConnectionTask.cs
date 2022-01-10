using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTask : Task
{
    public GameObject connector, connectible;
    public override void StartTask()
    {
        base.StartTask();
        connector = base.FindTool(connector.name);
        connectible = base.FindTool(connectible.name);
        if (connector != null && connectible != null)
        {
            connector.GetComponent<ConnectorObject>().connectorActive = true;
            connectible.GetComponent<Connectible>().SetConnector(connector.GetComponent<ConnectorObject>());
        }
    }
    // Checks the success condition
    public override bool IsSuccessful(CollisionEvent ce) => false;
}