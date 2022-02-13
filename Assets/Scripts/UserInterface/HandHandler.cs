using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class HandHandler : MonoBehaviour
{
    public GameObject controls; // on default disabled
    public GameObject right_ray_interactor; // on default enabled
    public GameObject left_ray_interactor; // on default enabled
    public InputActionReference toggleReferenceControl_right = null;

    private void Awake()
    {
        toggleReferenceControl_right.action.started += ShowControls;
    }

    private void OnDestroy()
    {
        toggleReferenceControl_right.action.started -= ShowControls;
    }
    public void ShowControls(InputAction.CallbackContext context)
    {
        bool isActive = !controls.activeSelf;
        controls.SetActive(isActive);

        var right_hand_ray = right_ray_interactor.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>();
        right_hand_ray.gameObject.SetActive(!isActive);

        var left_hand_ray = left_ray_interactor.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>();
        left_hand_ray.gameObject.SetActive(!isActive);
    }
}
