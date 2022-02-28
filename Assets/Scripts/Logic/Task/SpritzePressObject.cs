using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpritzePressObject : PressObject
{
    public InputActionReference toggleReferenceLeft = null;
    public InputActionReference toggleReferenceRight = null;
    private bool reingepumpt = false;

    private void Awake()
    {
        toggleReferenceLeft.action.started += Toggle;
        toggleReferenceRight.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReferenceLeft.action.started -= Toggle;
        toggleReferenceRight.action.started -= Toggle;
    }
    private void Toggle(InputAction.CallbackContext context)
    {
        if (!reingepumpt)
        {
            //  hier wird reingepumpt
            reingepumpt = true;
        }
        else
        {
            //  hier wird rausgepumpt und task beendet
            Press();
            GetComponent<ConnectorObject>().Disconnect();
        }
    }
}
