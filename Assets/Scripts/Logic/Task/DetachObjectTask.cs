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
    //für SPritze disconected nicht Bug
    private bool tryDisconect = false;
    public float tryDisconectTimer = 0.1f;
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

        //für SPritze disconected nicht Bug
        tryDisconect = true;

        /*
        if (c.GetIsGrabbed())
        {
            Debug.Log("3");
            connectorOb.Disconnect();
            isActive = false;
        }
        */
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

    //für den Spritze disconected nicht Bug
    public void Update()
    {
        if (tryDisconect)
        {
            ConnectorObject connectorOb = connectorObject.GetComponent<ConnectorObject>();
            Connectible c = connectible.GetComponent<Connectible>();
            if (c.GetIsGrabbed())
            {
                connectorOb.Disconnect();
                isActive = false;
                tryDisconect = false;
            }
            tryDisconectTimer -= Time.deltaTime;
            if (tryDisconectTimer <= 0)
            {
                tryDisconect = false;
            }
        }
    }
}
