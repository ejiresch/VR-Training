﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CufflineConnectionTask : Task
{
    public GameObject connector;
    public override void StartTask()
    {
        base.StartTask();
        connector = base.FindTool(connector.name);
        GameObject connectible = ProcessHandler.Instance.GetCompoundObject().GetComponent<CompundObject>().GetGrabbable();
        if (connector != null && connectible != null)
        {
            connector.GetComponent<ConnectorObject>().connectorActive = true;
            connectible.GetComponent<Connectible>().SetConnector(connector.GetComponent<ConnectorObject>());
        }
    }
    // Checks the success condition
    public override bool IsSuccessful(CollisionEvent ce) => false;
}