using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Behandelt alle Methoden die für das whiteboard notwendig sind */
public class WhiteboardHandler : MonoBehaviour
{
    public List<GameObject> tasklist;
    public GameObject task_prefab;

    private GameObject task_current; // derzeitiger Task
    private TextMeshProUGUI task_text_current; //text
    private SpriteRenderer checkmark_current; //bild

    private float maxTaskShown = 5; // Max anzahl an Tasks

    int i = 0; //test var

    public void pressButton()// Übungsmethode zum Testen
    {
        if(task_current == null)
        {
            FirstTask("jo " + i);
        }
        else
        {
            NewTask("Trachealkompresse unter die Kanüle schieben" + i);
        }
        i++;
    }

    public void FirstTask(string taskdescription) // Ersten Task erstellen
    {
        task_current = Instantiate(task_prefab, transform.GetChild(0)); // Child(0) ist "Panel_White"
        tasklist.Add(task_current);

        task_text_current = tasklist[0].GetComponentInChildren<TextMeshProUGUI>();
        checkmark_current = tasklist[0].GetComponentInChildren<SpriteRenderer>();

        task_text_current.SetText(taskdescription);
    }

    public void NewTask(string taskdescription) // Neuen Task erstellen
    {
        this.FinishTask();
        StartCoroutine(TaskRotation(taskdescription));
    }

    IEnumerator TaskRotation(string taskdescription) // Verschiebung der Tasks und das anzeigen des neuen Tasks
    {
        yield return new WaitForSeconds(0.4f);
        // Erstellung eines neuen Tasks
        task_current = Instantiate(task_prefab, transform.GetChild(0));
        task_current.SetActive(false);
        tasklist.Add(task_current);
        task_text_current = tasklist[tasklist.Count - 1].GetComponentInChildren<TextMeshProUGUI>();
        checkmark_current = tasklist[tasklist.Count - 1].GetComponentInChildren<SpriteRenderer>();
        task_text_current.SetText(taskdescription);

        // Verschiebung der Tasks auf der Y-Achse
        for (int i = tasklist.Count - 2; i >= 0; i--) 
        {
            /*
             * y_gab: Y-Abstand zwischen den Tasks
             * preferredHeight: Höhe des Textes
             * 0.0065f -> Ideale Abstandgröße für preferredHeight = 1 (von mir selbst gewählter Wert)
             */
            float y_gap = task_text_current.preferredHeight * 0.0065f; 
            tasklist[i].transform.position -= new Vector3(0f, y_gap, 0f);
        }
        if(tasklist.Count >= maxTaskShown) // Wenn das maximum an Tasks erreicht ist, wir das älteste Element gelöscht
        {
            Destroy(tasklist[0]);
            tasklist.Remove(tasklist[0]); 
        }

        yield return new WaitForSeconds(0.4f);

        task_current.SetActive(true);
    }

    public void FinishTask() // Stellt einen Task als fertig dar
    {
        task_text_current.color = new Color(0, 255, 0, 255); //Textfarbe auf grün

        Color tmp = checkmark_current.GetComponent<SpriteRenderer>().color; // Änderung der opasity auf 1 -> 100%
        tmp.a = 1f;
        checkmark_current.GetComponent<SpriteRenderer>().color = tmp;
    }
    
}
