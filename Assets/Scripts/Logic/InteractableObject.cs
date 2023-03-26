using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// Objects that have this script, send reports upon colliding with other interactibles
public class InteractableObject : MonoBehaviour, OnDropFunctions
{
    [SerializeField] private bool isGrabbed = false;
    private LayerMask lmNotGrabbable = 0;
    private LayerMask lmGrabbable = ~0;
    private List<Func<GameObject, bool>> onDropFunctions = new List<Func<GameObject, bool>>();
    public void SetGrabbable(bool grab)
    {
        XRGrabInteractable xrObject = gameObject.GetComponent<XRGrabInteractable>();
        xrObject.interactionLayerMask = grab ? lmGrabbable : lmNotGrabbable;
    }
    public virtual void OnDrop() 
    {
        ExecuteOnDropFunc();
    }

    public bool GetIsGrabbed()
    {
        return isGrabbed;
    }

    public virtual void SetIsGrabbed(bool isg)
    {
        this.isGrabbed = isg;
        if (!isg) OnDrop();
    }

    public void SetOnDropFunc(Func<GameObject, bool> func)
    {
        onDropFunctions.Add(func);
    }
    public void ResetOnDropFunc() => onDropFunctions = new List<Func<GameObject, bool>>();

    public void ExecuteOnDropFunc()
    {
        foreach (Func<GameObject, bool> func in onDropFunctions)
        {
            func(this.gameObject);
        }
        onDropFunctions = new List<Func<GameObject, bool>>();
    }
}
