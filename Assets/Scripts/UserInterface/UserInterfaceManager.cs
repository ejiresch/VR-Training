using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the UI
public class UserInterfaceManager : MonoBehaviour
{
    public WhiteboardHandler wbh;
    public HUDHandler hudh;
    public HandHandler hh;
    // New Tasks are displayed in this method
    public void NewTask(string taskdescription, bool isFirst)
    {
        if (isFirst) wbh.FirstTask(taskdescription);
        else
        {
            wbh.NewTask(taskdescription);
            hudh.ShowText(); // Anzeige von "Aufgabe abgeschlossen"
        }
    }
    // When all tasks are over
    public void EndOfTasks()
    {
        wbh.FinishTask();
        wbh.ShowEndMessage();
    }
    public void ShowWarning(int warningIndex) // Wird aufgerufen, wenn UI Beide Haende benutzen anzeigen soll
    {
        hh.Warning(warningIndex);
    }
}
