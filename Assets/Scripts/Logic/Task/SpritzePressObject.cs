using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


/* Ist für die Spritze Animation zuständig */
[RequireComponent(typeof(ResetManager))]
[RequireComponent(typeof(MaterialFetcher))]
public class SpritzePressObject : PressObject, ResetInterface
{
    public InputActionReference toggleReference = null;
    public bool reingepumpt = false; // spritze aufgezogen 
    public bool nurRauspumpen = false;
    public bool nurReinpumpen = false;
    public bool disconnectOnCompletion = false;
    public GameObject kolben;
    public GameObject ObjectToGetMaterial;
    private Animator anim;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        if (nurRauspumpen) StartCoroutine(Reinpumpen());
        if (PlayerPrefs.GetInt("resetCommand") == 1)
        {
            if (this.gameObject.GetComponent<ResetManager>())
            {
                this.gameObject.GetComponent<ResetManager>().Register(this);
            }
            else
            {
                Debug.LogError("Kein ReserManager am GameObject");
            }
        }
        
    }


    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        if (!pressable) return;
        if (!reingepumpt) StartCoroutine(Reinpumpen());
        else StartCoroutine(Rauspumpen());
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
        ResetManager resetm;
        if (resetm = GetComponent<ResetManager>())
        {
            resetm.ResetTool();
        }
        
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
