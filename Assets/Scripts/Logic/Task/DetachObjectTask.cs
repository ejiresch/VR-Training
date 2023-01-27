using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetachObjectTask : Task
{
    public InputActionReference toggleReference = null;
    public GameObject connectorObject;

    public override void StartTask()
    {
        base.StartTask();
        connectorObject = base.FindTool(connectorObject.name);

    }
    private void Toggle(InputAction.CallbackContext context)
    {
        ConnectorObject connectorOb;
        if (connectorOb = connectorObject.GetComponent<ConnectorObject>())
        {
            connectorOb.Disconnect();
            ProcessHandler.Instance.NextTask();
        }
        else
        {
            Debug.LogError("Objekt hat kein ConnectorObject Component!");
        }
    }
    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
}
