using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManometerMovement : PressObject
{
    public GameObject nadel;
    public InputActionReference toggleReference= null;

    private void Update()
    {
        if (nadel.transform.localRotation.eulerAngles.y > 32f)
        {
            nadel.transform.Rotate(new Vector3(0, -23f * Time.deltaTime, 0));   // Druck entlassen
        }  
    }
    private void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }
    private void Toggle(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        StartCoroutine(Druecken());
    }

    IEnumerator Druecken()
    {
        if (this.GetComponent<InteractableObject>().GetIsGrabbed())
        {
            enabled = false; // stoppt Script, damit update-function gestoppt wird -> Druecken wird aber weiter ausgeführt (warum auch immer)
            for (int i = 0; i < 50; i++)
            {
                if (nadel.transform.localRotation.eulerAngles.y >= 330) { break; }
                if(nadel.transform.localRotation.eulerAngles.y >= 140)
                {
                    Press();
                    GetComponent<ConnectorObject>().Disconnect();
                    break;
                }
                nadel.transform.Rotate(new Vector3(0, 1, 0));   // Druck hinzufügen
                yield return new WaitForSeconds(0.007f);
            }
            enabled = true; // activates Script
        }
    }
}
