using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Stellt ein Teil eines CompoundObjects dar
public class CompundPart : InteractableObject
{
    [SerializeField] private bool taskFocus;
    // Start is called before the first frame update
    void Awake() => taskFocus = false;
    // Wird aufgerufen wenn das Objekt losgelassen wird
    public override void OnDrop()
    {
        base.OnDrop();
        if (taskFocus)
        {
            ProcessHandler.Instance.NextTask();
            taskFocus = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    // Ob das Objekt aktiv sein soll, soll von der Task gesetzt werden
    public void SetTaskFocus(bool focus) => taskFocus = focus;
}
