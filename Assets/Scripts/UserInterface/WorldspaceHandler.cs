using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Behandelt alle Methoden die für den Worldspace notwendig sind */
public class WorldspaceHandler : MonoBehaviour
{
    public TextMeshProUGUI task; //text
    public GameObject checkmark; //bild


    public void pressButton()// Übungsmethode zum Testen
    {
        this.FinishTask();
        StartCoroutine(StartNewTask("Hello"));
    }

    public void NewTask(string taskdescription) // Wird von einer Methode in "Taskmanager" (oder so ka) ausgeführt.
    {
        this.FinishTask();
        Debug.Log("Next task: " + taskdescription);
        StartCoroutine(StartNewTask(taskdescription));

    }
    public void StartTask(string taskdescription) // Wird von einer Methode in "Taskmanager" (oder so ka) ausgeführt.
    {
        Debug.Log("Start task: " + taskdescription);
        StartCoroutine(StartNewTask(taskdescription));
    }
    public void FinishTask() // Stellt einen Task als fertig dar
    {
        task.color = new Color(0, 255, 0, 255); //green
        checkmark.SetActive(true);
    }
    IEnumerator StartNewTask(string taskdescription)
    {
        yield return new WaitForSeconds(1);
        task.gameObject.SetActive(false); // im besten fall ein Fade-Animation des Aufgabentextes
        checkmark.SetActive(false);
        yield return new WaitForSeconds(1);
        task.SetText(taskdescription);
        task.color = new Color(0, 0, 0, 255); //black
        task.gameObject.SetActive(true);
    }
}
