using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stellt ein Teil eines CompoundObjects dar
public class CompundPart : InteractableObject
{
    [SerializeField] private bool taskFocus;
    
    // Start is called before the first frame update
    void Awake()
    {
        taskFocus = false;
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;
    }
    // Wird aufgerufen wenn das Objekt losgelassen wird
    public override void OnDrop()
    {
        base.OnDrop();
        if (taskFocus)
        {
            
            taskfinished = true;
            taskFocus = false;
            GetComponent<Rigidbody>().isKinematic = false;
            
        }
    }

    //ACHTUNG ACHTUNG BISSCHEN SO AUF TEST
    public void GrabableUpdate()
    {
        if (taskFocus)
        {
            GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
        }
    }


    // Ob das Objekt aktiv sein soll, soll von der Task gesetzt werden
    public void SetTaskFocus(bool focus)
    {
        taskFocus = focus;
        GrabableUpdate();
    }
}
