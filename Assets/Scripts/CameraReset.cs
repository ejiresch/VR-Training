using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraReset : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public Transform cameraTransform; // Assign Main Camera here

    private InputDevice _targetDevice;
    private Vector3 _startPosition;

    private bool _wasPressed = false;

    void Start()
    {
        _startPosition = transform.position;
        InitializeDevice();
    }

    void Update()
    {
        if (!_targetDevice.isValid)
        {
            InitializeDevice();
            return;
        }

        if (_targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressed))
        {
            if (pressed && !_wasPressed)
            {
                Recenter();
            }

            _wasPressed = pressed;
        }
    }

    void InitializeDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + "\n");
        }

        if (devices.Count > 0)
        {
            _targetDevice = devices[0];
        }
    }

    void Recenter()
    {
        // How far the head moved inside tracking space
        Vector3 headOffset = cameraTransform.localPosition;

        // Move XR Origin opposite to head offset
        transform.position = _startPosition - new Vector3(headOffset.x, 0, headOffset.z);

        Debug.Log("Recentered XR Origin");
    }
}
