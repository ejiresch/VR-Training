using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Definiert ein Objekt, welches gedrueckt werden muss
public class PressObject : InteractableObject
{
    public bool pressable = false;
    // Soll von Unterklassen aufgerufen werden wenn die Task beendet werden soll
    public virtual void Press()
    {
        if (pressable)
        {
            ProcessHandler.Instance.NextTask();
            pressable = false;
        }
    }
    // Ob das Objekt drueckbar sein soll
    public virtual void SetPressable(bool pressable)
    {
        this.pressable = pressable;
    }
}
