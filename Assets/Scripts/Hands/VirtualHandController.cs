
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

[InputControlLayout(displayName = "Virtual Hand Controller")]
public class VirtualHandController : XRController
{
    [InputControl(layout = "Axis")]
    public AxisControl trigger { get; private set; }

    [InputControl(layout = "Button")]
    public ButtonControl triggerPressed { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        trigger = GetChildControl<AxisControl>("trigger");
        triggerPressed = GetChildControl<ButtonControl>("triggerPressed");
    }
}
