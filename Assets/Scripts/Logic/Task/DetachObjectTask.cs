using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// 
/// </summary>
public class DetachObjectTask : Task
{
    public InputActionReference toggleReference = null;
    public GameObject connectorObject;
    public GameObject connectible;
    private bool isActive = false;
    private bool tryDisconnect = false;
    private float tryDisconnectTimer = 0.1f;
    public override void StartTask()
    {
        base.StartTask();
        connectorObject = base.FindTool(connectorObject.name);
        connectible = base.FindTool(connectible.name);
        connectible.GetComponent<Connectible>().SetConnector(null); // Wichtig, da sonst ein Preview erzeugt wird
        connectible.GetComponent<Connectible>().SetGrabbable(true); // Muss Grabbable sein 
        connectible.GetComponent<XRBaseInteractable>().interactionLayerMask = ~0;
        foreach (Collider collider in connectible.GetComponentsInChildren<Collider>()) collider.enabled = true;
        isActive = true;
    }
    private void Toggle(InputAction.CallbackContext context)
    {

        if (!isActive) return;
        tryDisconnectTimer = 0.1f;

        //Da es zu einem Bug kommen kann, wenn in der Toggle-Methode nur einmal überprüft wird ob das Connectible auf IsGrabbed true gesetzt ist, muss es über mehrere Frames abgefragt werden.
        //Durch try Disconnect = true kann Code in der Update Methode ausgeführt werden
        tryDisconnect = true;

    }
    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;

    protected override void CompReset()
    {
        connectorObject.GetComponent<ConnectorObject>().SetTaskFinished(false);
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!connectorObject.GetComponent<ConnectorObject>().GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
    
    public void Update()
    {
        base.Update();
        //solange tryDisconnectTimer über 0 ist wird in jedem Frame überprüft ob das Connectible auf IsGrabbed true gesetzt ist, dann wird weiterer Code im Connector ausgeführt um den Task schließlich abzuschließen. Dies ist notwendig da SetIsGrabbed beim Connectible immer zu einer unterschiedlichen, zufälligen Zeit aufgerufen wird.
        if (tryDisconnect)
        {
            ConnectorObject connectorOb = connectorObject.GetComponent<ConnectorObject>();
            Connectible c = connectible.GetComponent<Connectible>();
            if (c.GetIsGrabbed())
            {
                connectorOb.Disconnect();
                isActive = false;
                tryDisconnect = false;
            }
            tryDisconnectTimer -= Time.deltaTime;
            if (tryDisconnectTimer <= 0)
            {
                tryDisconnect = false;
            }
        }
    }
}
