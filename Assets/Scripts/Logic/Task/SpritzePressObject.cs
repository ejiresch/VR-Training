using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


/* Ist für die Spritze Animation zuständig */
[RequireComponent(typeof(MaterialFetcher))]
public class SpritzePressObject : PressObject, ResetInterface
{
    public InputActionReference toggleReference = null;
    [HideInInspector]public bool reingepumpt = false; // spritze aufgezogen 
    [HideInInspector] public bool nurRauspumpen = false;
    [HideInInspector] public bool nurReinpumpen = false;
    [HideInInspector] public bool disconnectOnCompletion = false;
    public GameObject kolben;
    private Animator anim;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        if (nurRauspumpen) StartCoroutine(Reinpumpen());     
    }


    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        if (GetIsGrabbed())
        {
            if (!pressable) return;
            if (!reingepumpt) StartCoroutine(Reinpumpen());
            else StartCoroutine(Rauspumpen());
        }
    }
    public override void Press()
    {
        if (!pressable) return;
        if (disconnectOnCompletion)
        {
            ConnectorObject connObj;
            if(connObj = GetComponent<ConnectorObject>())
            {
                connObj.Disconnect();  //disconnect object
            }
            else
            {
                GetComponent<Connectible>().GetConnector().Disconnect();  //disconnect object
            }

        }
        if (!nurRauspumpen)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("CompoundGrabbablePart").transform.parent.parent.gameObject;
            temp.GetComponent<InteractableObject>().SetGrabbable(true);
            temp.GetComponent<Rigidbody>().isKinematic = false;
        }
        base.Press();   //next task
        
    }
    IEnumerator Reinpumpen() // Start der "Reinpumpen" Animation
    {
        if(GetComponent<Connectible>())
            if (GetComponent<Connectible>().GetConnector())
            {
                GetComponent<MaterialFetcher>().MaterialChange(gameObject, GetComponent<Connectible>().GetConnector().gameObject, "liquid");
            }
        anim.SetTrigger("reinpumpen");
        yield return new WaitForSeconds(1.1f);
        reingepumpt = true;
        if (nurReinpumpen)
        {
            Press();
        }
    }
    IEnumerator Rauspumpen() // Start der "Rauspumpen" Animation
    {
       
        anim.SetTrigger("rauspumpen");
        yield return new WaitForSeconds(1.3f);
        reingepumpt = false;
        Press();
    }
    public void ResetComp()
    {
        
    }
    public bool[] SetConfig(bool[] nurReinpumpen)
    {
        return nurReinpumpen;
    }
}
