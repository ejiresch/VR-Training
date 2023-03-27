using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ClipPressObject : PressObject
{
    public InputActionReference toggleReference = null;
    private bool isGrabbed = false;
    private bool allDone = false;
    private Transform parent;
    private Func<GameObject, bool> ondrop;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;

    }
    private void Awake() => toggleReference.action.started += Toggle;
    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        if (this.isGrabbed)
        {
            allDone = true;    
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetIsGrabbed(bool grabbed)
    {
        this.isGrabbed = grabbed;
    }
    public override void SetPressable(bool pressable)
    {
        base.SetPressable(pressable);
        this.gameObject.transform.parent = ProcessHandler.Instance.transform;
        GetComponent<XRGrabInteractable>().interactionLayerMask = ~0;
    }
    public void AllDoneDrop()
    {
        if (allDone)
        {
            transform.parent = parent;
            GetComponent<XRGrabInteractable>().interactionLayerMask = 0;
            Press();
            allDone = false;
        }   
    }
}
