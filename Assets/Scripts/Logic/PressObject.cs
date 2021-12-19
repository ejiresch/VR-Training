using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressObject : InteractableObject
{
    private bool pressable = false;

    public void Press()
    {
        if (pressable)
        {
            ProcessHandler.Instance.NextTask();
            pressable = false;
        }
    }

    public void SetPressable(bool pressable)
    {
        this.pressable = pressable;
    }
}
