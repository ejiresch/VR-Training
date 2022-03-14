using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressObject : MonoBehaviour
{
    public bool pressable = false;

    public virtual void Press()
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
