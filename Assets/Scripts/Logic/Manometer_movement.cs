using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manometer_movement : MonoBehaviour
{
    public GameObject nadel;
    public InputActionReference toggleReferenceLeft = null;
    public InputActionReference toggleReferenceRight = null;

    private void Update()
    {
        if (nadel.transform.rotation.eulerAngles.y > 32f)
        {
            nadel.transform.Rotate(new Vector3(0, -23f * Time.deltaTime, 0));   // Druck entlassen
        }  
    }
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
        StopAllCoroutines();
        Debug.LogError("Toggle wird ausgefuehrt");
        StartCoroutine(Druecken());
    }

    IEnumerator Druecken()
    {
        if (this.GetComponent<InteractableObject>().GetIsGrabbed() == true)
        {
            enabled = false; // stoppt Script, damit update-function gestoppt wird -> Druecken wird aber weiter ausgeführt (warum auch immer)
            for (int i = 0; i < 50; i++)
            {
                if (nadel.transform.rotation.eulerAngles.y >= 330) { break; }
                nadel.transform.Rotate(new Vector3(0, 1, 0));   // Druck hinzufügen
                yield return new WaitForSeconds(0.007f);
            }
            enabled = true; // activates Script
        }
        GetComponent<PressObject>().Press();
    }
}
