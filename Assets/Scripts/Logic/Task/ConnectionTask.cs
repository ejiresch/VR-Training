using System.Collections;
using System.Collections.Generic;
using development_a;
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

    protected override void CompReset()
    {
        connector.GetComponent<ConnectorObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!connector.GetComponent<ConnectorObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        FindObjectOfType<SoundManager>().ManageSound("Plop", true, 1f);
        EndTask();
        if (Input.GetButtonDown("Jump"))
        {
            connector.GetComponent<ConnectorObject>().SetTaskFinished(true);
        }
    }
}