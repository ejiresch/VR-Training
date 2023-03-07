using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DetachObjectTask : Task
{
    public InputActionReference toggleReference = null;
    public GameObject connectorObject;
    public GameObject connectible;
    private bool isActive = false;
    public override void StartTask()
    {
        base.StartTask();
        connectorObject = base.FindTool(connectorObject.name);
        connectible = base.FindTool(connectible.name);
        connectible.GetComponent<Connectible>().SetConnector(null);
        connectible.GetComponent<Connectible>().SetGrabbable(true);
        isActive = true;
    }
    private void Toggle(InputAction.CallbackContext context)
    {
        if (!isActive) return;
        ConnectorObject connectorOb = connectorObject.GetComponent<ConnectorObject>();
        if (connectible.GetComponent<Connectible>().GetIsGrabbed())
        {
            connectorOb.Disconnect();
            ProcessHandler.Instance.NextTask();
            isActive = false;
        }
    }
    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
}
