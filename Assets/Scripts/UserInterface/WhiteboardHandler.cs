using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/* Behandelt alle Methoden die für das whiteboard notwendig sind */
public class WhiteboardHandler : MonoBehaviour
{
    public List<GameObject> tasklist;
    public GameObject task_prefab;

    private GameObject task_current; // derzeitiger Task
    private TextMeshProUGUI task_text_current; //text
    private SpriteRenderer checkmark_current; //bild

    private float maxTaskShown; // Max anzahl an Tasks
    private int task_number;

    int i = 0; //test var

    public void Start()
    {
        maxTaskShown = 5;
        task_number = 1;
    }
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
        task_current = Instantiate(task_prefab, transform.GetChild(1)); // Child(1) ist "Vertical_Layout_Group"
        tasklist.Add(task_current);
        task_current.transform.position -= new Vector3(0f, 0.05f, 0f);

        task_text_current = tasklist[0].GetComponentInChildren<TextMeshProUGUI>();
        checkmark_current = tasklist[0].GetComponentInChildren<SpriteRenderer>();

        task_text_current.SetText(task_number + ". " + taskdescription);
        task_number++;
    }

    public void NewTask(string taskdescription) // Neuen Task erstellen
    {
        this.FinishTask();
        StartCoroutine(TaskRotation(task_number + ". " + taskdescription));
        task_number++;
    }

    IEnumerator TaskRotation(string taskdescription) // Verschiebung der Tasks und das anzeigen des neuen Tasks
    {
        yield return new WaitForSeconds(0.4f);
        // Erstellung eines neuen Tasks
        task_current = Instantiate(task_prefab, transform.GetChild(1));

        task_current.SetActive(false);
        tasklist.Add(task_current);
        task_text_current = tasklist[tasklist.Count - 1].GetComponentInChildren<TextMeshProUGUI>();
        checkmark_current = tasklist[tasklist.Count - 1].GetComponentInChildren<SpriteRenderer>();
        task_text_current.SetText(taskdescription);

        if (tasklist.Count >= maxTaskShown) // Wenn das maximum an Tasks erreicht ist, wir das älteste Element gelöscht
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
    // Buttons: 
    public void ReingebenStarten()
    {
        ProcessHandler.Instance.SetProcessIndex(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RausnehmenStarten()
    {
        ProcessHandler.Instance.SetProcessIndex(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
