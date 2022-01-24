using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldspaceHandler : MonoBehaviour
{
    public GameObject controls;
    public InputActionReference toggleReferenceControl = null;

    public GameObject camera;
    Quaternion cameraStartRotation;

    public void Start()
    {
        cameraStartRotation = camera.transform.rotation;
    }
    private void Awake()
    {
        toggleReferenceControl.action.started += ShowControls;
    }

    private void OnDestroy()
    {
        toggleReferenceControl.action.started -= ShowControls;
    }
    public void ShowControls(InputAction.CallbackContext context)
    {
        controls.transform.position = camera.transform.position + (camera.transform.forward * 2);
        controls.transform.eulerAngles += new Vector3(0, camera.transform.rotation.y, 0);
        bool isActive = !controls.activeSelf;
        controls.SetActive(isActive);
    }
}
