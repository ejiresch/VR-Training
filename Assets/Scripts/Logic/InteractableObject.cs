﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// Objects that have this script, send reports upon colliding with other interactibles
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isGrabbed = false;

    private LayerMask lmNotGrabbable = 0;
    private LayerMask lmGrabbable = ~0;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            CollisionEvent ce = new SimpleCollisionEvent(this.gameObject, collision.gameObject);
            ce.ReportCollision();
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnDrop();
        }
    }

    public void SetGrabbable(bool grab)
    {
        XRGrabInteractable xrObject = gameObject.GetComponent<XRGrabInteractable>();
        xrObject.interactionLayerMask = grab ? lmGrabbable : lmNotGrabbable;
    }
    public virtual void OnDrop() { }

    public bool GetIsGrabbed()
    {
        return isGrabbed;
    }

    public virtual void SetIsGrabbed(bool isg)
    {
        this.isGrabbed = isg;
        if (!isg) OnDrop();
    }
}