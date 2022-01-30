using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Quellen: 
//https://learn.unity.com/tutorial/customizing-interactables-in-xr-interaction-toolkit#602ed568edbc2a2625047887
//https://www.youtube.com/watch?v=G3vKyFXjk1I&ab_channel=DanielStringer (ab 4:43)


public class InteractableHapticFeedback : MonoBehaviour
{
    private XRGrabInteractable m_GrabInteractable;
    public XRDirectInteractor rechts;
    public XRDirectInteractor links;

    private void Awake()
    {
        print("Awake() wurde aufgerufen");
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        m_GrabInteractable.onSelectExited.AddListener(PlaySelectExitVibration);
    }

    private void PlaySelectExitVibration(XRBaseInteractor obj)
    {
        if (true)
        {
            rechts.SendHapticImpulse(0.4f, 0.1f);
            links.SendHapticImpulse(0.4f, 0.1f);
        }
        else
        {
            rechts.SendHapticImpulse(1f, 0.3f);
            links.SendHapticImpulse(1f, 0.3f);
        }
    }

    
}
