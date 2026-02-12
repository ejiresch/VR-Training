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
    [HideInInspector] public bool reingepumpt = false; // spritze aufgezogen
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

    private void Awake()
    {
        // MINI-FIX: null-safe
        if (toggleReference != null && toggleReference.action != null)
            toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        // MINI-FIX: null-safe
        if (toggleReference != null && toggleReference.action != null)
            toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
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

        ConnectorObject conn = GetComponent<ConnectorObject>();
        if (conn != null && !conn.HasConnection())
            return;

        Connectible c0 = GetComponent<Connectible>();
        if (c0 != null)
        {
            ConnectorObject connector0 = c0.GetConnector();
            if (connector0 != null && !connector0.HasConnection())
                return;
        }

        if (disconnectOnCompletion)
        {
            // MINI-FIX: Disconnect nur wenn Referenzen existieren
            ConnectorObject connObj = GetComponent<ConnectorObject>();
            if (connObj != null)
            {
                connObj.Disconnect();
            }
            else
            {
                Connectible c = GetComponent<Connectible>();
                if (c != null && c.GetConnector() != null)
                {
                    c.GetConnector().Disconnect();
                }
                // wenn null: in der Luft -> einfach nichts tun
            }
        }

        if (!nurRauspumpen)
        {
            // MINI-FIX: FindGameObjectWithTag kann null liefern
            GameObject tagged = GameObject.FindGameObjectWithTag("CompoundGrabbablePart");
            if (tagged != null && tagged.transform.parent != null && tagged.transform.parent.parent != null)
            {
                GameObject temp = tagged.transform.parent.parent.gameObject;
                temp.GetComponent<InteractableObject>().SetGrabbable(true);
                temp.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        base.Press(); // next task
    }

    IEnumerator Reinpumpen()
    {
        if (GetComponent<Connectible>())
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

    IEnumerator Rauspumpen()
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