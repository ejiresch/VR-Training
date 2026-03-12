using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Objects that have this script, send reports upon colliding with other interactibles
[RequireComponent(typeof(InteractableHandler))]
public class InteractableObject : MonoBehaviour, OnDropFunctions
{
    [SerializeField] private bool isGrabbed = false;
    private LayerMask lmNotGrabbable = 0;
    private LayerMask lmGrabbable = ~0;
    private List<Func<GameObject, bool>> onDropFunctions = new List<Func<GameObject, bool>>();
    protected bool taskfinished = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable xrObject;
    public void SetGrabbable(bool grab)
    {
        if (xrObject == null)
        {
            xrObject = gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        }
        xrObject.interactionLayers = grab ? lmGrabbable.value : lmNotGrabbable.value;
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
    public void SetTaskFinished(bool isF)
    {
        taskfinished = isF;
    }
    public bool GetTaskCompletion() => taskfinished;
}
