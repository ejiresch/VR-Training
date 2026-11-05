
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class HandControllerInputSimulator : MonoBehaviour
{
    private VirtualHandController leftController;
    private VirtualHandController rightController;

    private float lastLeftValue = -1f;
    private float lastRightValue = -1f;

    private static bool layoutRegistered;

    private void Awake()
    {
        // Register our custom controller layout.
        if (!layoutRegistered)
        {
            InputSystem.RegisterLayout<VirtualHandController>();
            layoutRegistered = true;
        }

        // Create left virtual controller.
        leftController = InputSystem.AddDevice<VirtualHandController>();
        InputSystem.SetDeviceUsage(leftController, "LeftHand");

        // Create right virtual controller.
        rightController = InputSystem.AddDevice<VirtualHandController>();
        InputSystem.SetDeviceUsage(rightController, "RightHand");

        Debug.Log("Virtual hand controllers created!");
    }

    // Set left trigger (0 = released, 1 = pressed).
    public void SetLeftTrigger(float value)
    {
        SetTrigger(leftController, value, ref lastLeftValue);
    }

    // Set right trigger (0 = released, 1 = pressed).
    public void SetRightTrigger(float value)
    {
        SetTrigger(rightController, value, ref lastRightValue);
    }

    // Boolean convenience methods.
    public void LeftTrigger(bool pressed)
    {
        SetLeftTrigger(pressed ? 1f : 0f);
    }

    public void RightTrigger(bool pressed)
    {
        SetRightTrigger(pressed ? 1f : 0f);
    }

    private void SetTrigger(
        VirtualHandController controller,
        float value,
        ref float lastValue)
    {
        if (controller == null)
            return;

        value = Mathf.Clamp01(value);

        // Don't send duplicate events.
        if (Mathf.Approximately(value, lastValue))
            return;

        lastValue = value;

        // Create an event based on the device's current state.
        using (StateEvent.From(controller, out var eventPtr))
        {
            // Analog trigger value.
            controller.trigger.WriteValueIntoEvent(
                value, eventPtr);

            // Digital trigger button.
            controller.triggerPressed.WriteValueIntoEvent(
                value >= 0.5f ? 1f : 0f, eventPtr);

            // Send the event through Unity's Input System.
            InputSystem.QueueEvent(eventPtr);
        }
    }

    private void OnDestroy()
    {
        if (leftController != null &&
            leftController.added)
        {
            InputSystem.RemoveDevice(leftController);
        }

        if (rightController != null &&
            rightController.added)
        {
            InputSystem.RemoveDevice(rightController);
        }
    }
}
