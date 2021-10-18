using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Behandelt alle Methoden die für das corkboard notwendig sind */
public class CorkboardHandler : MonoBehaviour
{
    public TextMeshProUGUI task_text; //text
    public GameObject checkmark; //bild


    public void pressButton()// Übungsmethode zum Testen
    {
        this.FinishTask();
        StartCoroutine(SwitchTask("Hello"));
    }

    public void FirstTask(string taskdescription)
    {
        task_text.SetText(taskdescription);
    }

    public void NewTask(string taskdescription) // Neuen Task erstellen
    {
        this.FinishTask();
        StartCoroutine(SwitchTask(taskdescription));
    }

    IEnumerator SwitchTask(string taskdescription) // Übergang von Tasks
    {
        yield return new WaitForSeconds(1);
        task_text.gameObject.SetActive(false); // im besten fall ein Fade-Animation des Aufgabentextes
        checkmark.SetActive(false);
        yield return new WaitForSeconds(1);
        task_text.SetText(taskdescription);
        task_text.color = new Color(0, 0, 0, 255); //black
        task_text.gameObject.SetActive(true);
    }

    public void FinishTask() // Stellt einen Task als fertig dar
    {
        task_text.color = new Color(0, 255, 0, 255); //green
        checkmark.SetActive(true);
    }
    
}
