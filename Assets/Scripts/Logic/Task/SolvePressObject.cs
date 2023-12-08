using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


/* Ist für die Med Crush Animation zuständig */
public class SolvePressObject : PressObject, ResetInterface
{
    public InputActionReference toggleReference = null;
    public float maxDistance = 0.05f;   // Maximal Distanz zwischen Glas und Medikament

    [HideInInspector]public bool crushed = false; 
    private Animator anim;
    private GameObject med;
    private float dist;


    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        med = GameObject.FindWithTag("Medikament");
    }


    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        if (GetIsGrabbed())
        {
            dist = Vector3.Distance(transform.position, med.transform.position);   //  Distanz zwischen Glas und Medikament
            if (dist <= maxDistance)
            {
                if (!pressable) return;
                if (!crushed) StartCoroutine(Solve());
            }
        }
    }
    public override void Press()
    {
        if (!pressable) return;
        
        base.Press();   //next task
        
    }
    IEnumerator Solve() // Start der "Solve" Animation
    {
        anim.SetTrigger("solved");
        GameObject go = GameObject.Find("Medikament");
        if (go != null)
        {
            Destroy(go);
        }
        yield return new WaitForSeconds(1.3f);
        crushed = true;
        Press();
    }
    public void ResetComp()
    {
        
    }
}
