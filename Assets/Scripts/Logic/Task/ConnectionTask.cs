using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Eine Task welche fuer das Verbinden von einem ConnectorObject und Connectible zustaendig ist
public class ConnectionTask : Task
{
    public GameObject connector, connectible;
    // Wird am Anfang der Task ausgefuehrt
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
    //Dazu, Simon
    public override List<GameObject> HighlightedObjects()
    {
        List<GameObject> result = new List<GameObject>();
        result.Add(connectible);
        result.Add(connector);
        return result;
    }
}