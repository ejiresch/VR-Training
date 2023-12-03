using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Schnittstelle zwischen Prozesshandler und UI Scripts */
public class UserInterfaceManager : MonoBehaviour
{
    public WhiteboardHandler wbh;
    public HUDHandler hudh;
    public HandHandler hh;
    public void NewTask(string taskdescription, bool isFirst) // Wird aufgerufen, wenn ein neuer Task gestartet wird
    {
        if (isFirst) wbh.FirstTask(taskdescription);
        else
        {
            StartCoroutine(wbh.TaskRotation(taskdescription, true));
            hudh.ShowText(); // Anzeige von "Aufgabe abgeschlossen"
        }
    }

    public void NewTask(string taskdescription, bool isFirst, bool showWhiteboard) // Wird aufgerufen, wenn ein neuer Task gestartet wird der nicht am Whiteboard gezeigt werden soll
    {
        if (isFirst) wbh.FirstTask(taskdescription, showWhiteboard);
        else
        {
            StartCoroutine(wbh.TaskRotation(taskdescription, showWhiteboard));
            hudh.ShowText(); // Anzeige von "Aufgabe abgeschlossen"
        }
    }

    public void EndOfTasks()  // Wird aufgerufen, wenn alle Tasks beendet wurden
    {
        StartCoroutine(wbh.ShowEndMessage());
    }
    public void ShowWarning(int warningIndex) // Wird aufgerufen, wenn UI Beide Haende benutzen anzeigen soll
    {
        hh.Warning(warningIndex);
    }
}
