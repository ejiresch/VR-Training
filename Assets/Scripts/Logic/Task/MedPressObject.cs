using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


/* Ist für die Med Crush Animation zuständig */
public class MedPressObject : PressObject, ResetInterface
{
    public InputActionReference toggleReference = null;
    [HideInInspector]public bool crushed = false; 
    private Animator anim;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();  
    }


    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        if (GetIsGrabbed())
        {
            if (!pressable) return;
            if (!crushed) StartCoroutine(Crush());
        }
    }
    public override void Press()
    {
        if (!pressable) return;
        
        base.Press();   //next task
        
    }
    IEnumerator Crush() // Start der "Crush" Animation
    {
       
        anim.SetTrigger("crushed");
        yield return new WaitForSeconds(1.3f);
        crushed = true;
        Press();
    }
    public void ResetComp()
    {
        
    }
}
