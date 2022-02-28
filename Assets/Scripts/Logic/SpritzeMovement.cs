using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpritzeMovement : PressObject
{
    public GameObject kolben;
    public InputActionReference toggleReferenceLeft = null;
    public InputActionReference toggleReferenceRight = null;

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
        StartCoroutine(Druecken());
    }

    IEnumerator Druecken()
    {
        if (this.GetComponent<InteractableObject>().GetIsGrabbed())
        {
            
            for (int i = 0; i < 50; i++)
            {
                if (kolben.transform.position.y < 6) { break; }
                kolben.transform.position -= new Vector3(0, 0.05f, 0);   // Druck hinzufügen
                yield return new WaitForSeconds(0.007f);
            }
        }
    }
}
