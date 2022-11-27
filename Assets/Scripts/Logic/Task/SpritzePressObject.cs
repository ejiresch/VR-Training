using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Ist für die Spritze Animation zuständig */
public class SpritzePressObject : PressObject
{
    public InputActionReference toggleReference = null;
    public bool reingepumpt = false;
    public bool nurRauspumpen = false;
    public bool nurReinpumpen = false;
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
        if (!pressable) return;
        if (!reingepumpt) StartCoroutine(Reinpumpen());
        else StartCoroutine(Rauspumpen());
    }
    public override void Press()
    {
        if (!pressable) return;
        try
        {
            GetComponent<ConnectorObject>().Disconnect();   //disconnect object
        }
        catch
        {
            GetComponent<Connectible>().GetConnector().Disconnect(); //disconnect object
        }   //disconnect object
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
        Debug.Log("ReinPump");
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
        Debug.Log("RausPump");
        anim.SetTrigger("rauspumpen");
        yield return new WaitForSeconds(1.3f);
        Press();
    }
}
