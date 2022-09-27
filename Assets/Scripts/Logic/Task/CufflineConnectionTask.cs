using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine Connection Task, welche spezifisch fuer die Verbindung mit der Cuffline zustaendig ist
public class CufflineConnectionTask : Task
{
    public GameObject connector;
    // Wird beim Start der Task ausgefuehrt
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