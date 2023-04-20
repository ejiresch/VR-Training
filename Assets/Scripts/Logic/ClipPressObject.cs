using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
[Obsolete]
public class ClipPressObject : PressObject
{
    public InputActionReference toggleReference = null;
    private Transform parent;
    private Func<GameObject, bool> ondrop;
    private bool animationPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;

    }
    private void Awake() => toggleReference.action.started += Toggle;
    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        Animator animator = GetComponent<Animator>();
        bool close = animator.GetBool("KlippAufBool");

        if (GetIsGrabbed())
        {
            if (GetComponent<Animator>() && !animationPlayed)
            {
                if (!close)
                {
                   animator.SetBool("KlippAufBool", true);
                }
                else
                {
                   animator.SetBool("KlippAufBool", false);
                }
                animationPlayed = true;
                    
            }
            taskfinished = true;
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetPressable(bool pressable)
    {
        base.SetPressable(pressable);
    }

    public override void SetIsGrabbed(bool isg)
    {
        base.SetIsGrabbed(isg);
        animationPlayed = false;
    }
}
