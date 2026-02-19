using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/**
 * Resets Camera Postion with the press of the menu Button (left controller, the button withe the 3 stripes)
 * 
 * @author Tobias Salomon
 * @version 19-2-2026
 */
public class CameraReset : MonoBehaviour
{
    // what characteristic to check for
    public InputDeviceCharacteristics controllerCharacteristics;

    //transform of main Camera
    public Transform cameraTransform;

    //Input device wich it gets data from
    private InputDevice _targetDevice;

    //postion to where it recenters
    private Vector3 _startPosition;

    private bool _wasPressed = false;

    void Start()
    {
        //saves start position and gets devices
        _startPosition = transform.position;
        InitializeDevice();
    }

    void Update()
    {

        //checks if targetDevice is valid -> if not then reinintialize
        if (!_targetDevice.isValid)
        {
            InitializeDevice();
            return;
        }

        //gets input from secondary button and saves it into a tmp bool
        if (_targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed))
        {
            //checks if button was pressed and not pressed the frame earlier
            if (pressed && !_wasPressed)
            {
                Recenter();
            }

            //makes sure you don't get reset multiple times in a short time frame
            _wasPressed = pressed;
        }
    }

    void InitializeDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        /*
        foreach (var device in devices)
        {
            Debug.Log(device.name + "\n");
        }
        */

        if (devices.Count > 0)
        {
            _targetDevice = devices[0];
        }
    }

    //Recenters the user
    void Recenter()
    {
        // How far the head moved inside tracking space
        Vector3 headOffset = cameraTransform.localPosition;

        // Move XR Origin opposite to head offset
        transform.position = _startPosition - new Vector3(headOffset.x, 0, headOffset.z);

        //Debug.Log("Recentered XR Origin");
    }
}
