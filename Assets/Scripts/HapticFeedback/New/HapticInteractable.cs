using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]

public class Haptic 
{
    [Range(0, 1)]
    public float intensity;
    public float duration;

    public float intensityOff = 0.0f;
    public float durationOff = 0.0f;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
            //Debug.Log("Haptik");
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (!ProcessHandler.Instance.GetVibrationActive())
        {
            if (intensity > 0)
            {
                controller.SendHapticImpulse(intensity, duration);
                //Debug.Log("Haptik");
                
            }
        }
        else
        {
            controller.SendHapticImpulse(intensityOff, durationOff);
        }
    }
}

public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticOnActivated;
    public Haptic hapticHoverEntered;
    public Haptic hapticHoverExited;
    public Haptic hapticSelectEntered;
    public Haptic hapticSelectExited;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActivated.TriggerHaptic);
        interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic);
        interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic);
        interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
        interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic);
    }
}
