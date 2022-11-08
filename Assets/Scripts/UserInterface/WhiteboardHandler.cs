using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/* Behandelt alle Methoden die für das Whiteboard notwendig sind */
public class WhiteboardHandler : MonoBehaviour
{
    public List<GameObject> tasklist;
    public GameObject task_prefab; // Prefab fürs Task-UI Element

    private GameObject task_current; // derzeitiger Task
    private TextMeshProUGUI task_text_current; // text vom Task
    private SpriteRenderer checkmark_current; // checkmark-bild vom Task

    private float maxTaskShown = 4; // Max anzahl an Tasks
    private int task_number = 1;
    private int processGroup = 1;

    public void FirstTask(string taskdescription) // Ersten Task erstellen
    {
        this.NewTask(task_number + ". " + taskdescription, true);
        task_current.SetActive(true);
    }
    public void NewTask(string taskdescription, bool first) // Neuen Task erstellen
    {
        task_current = Instantiate(task_prefab, transform.GetChild(1));
        task_current.SetActive(false);
        tasklist.Add(task_current);
        if (first)
        {
            task_text_current = tasklist[0].GetComponentInChildren<TextMeshProUGUI>();
            checkmark_current = tasklist[0].GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            task_text_current = tasklist[tasklist.Count - 1].GetComponentInChildren<TextMeshProUGUI>();
            checkmark_current = tasklist[tasklist.Count - 1].GetComponentInChildren<SpriteRenderer>();
        }
        task_text_current.SetText(taskdescription);

        Color tmp = checkmark_current.GetComponent<SpriteRenderer>().color; // Änderung der opasity auf 0 -> 0%
        tmp.a = 0f;
        checkmark_current.GetComponent<SpriteRenderer>().color = tmp;

        if (tasklist.Count > maxTaskShown) // Wenn das maximum an Tasks erreicht ist, wir das älteste Element gelöscht
        {
            Destroy(tasklist[0]);
            tasklist.Remove(tasklist[0]);
        }
        task_number++;
    }
    public IEnumerator TaskRotation(string taskdescription) // Verschiebung der Tasks und das anzeigen des neuen Tasks
    {
        this.FinishTask();
        yield return new WaitForSeconds(0.4f);

        this.NewTask(task_number + ". " + taskdescription, false);

        yield return new WaitForSeconds(0.4f);
        task_current.SetActive(true);
    }
    public void FinishTask() // Stellt den derzeitigen Task als fertig dar
    {
        task_text_current.color = new Color(0, 255, 0, 255); //Textfarbe auf grün

        Color tmp = checkmark_current.GetComponent<SpriteRenderer>().color; // Änderung der opasity des checkmarks auf 1 -> 100%
        tmp.a = 1f;
        checkmark_current.GetComponent<SpriteRenderer>().color = tmp;
    }
    public IEnumerator ShowEndMessage() // Anzeige "Alles fertig,...". wenn alle Aufgaben abgeschlossen wurden
    {
        this.FinishTask();
        yield return new WaitForSeconds(0.4f);

        this.NewTask("Alles fertig, Herzlichen Glückwunsch!", false);

        yield return new WaitForSeconds(0.4f);
        task_current.SetActive(true);
        this.FinishTask();
    }
    public void StartProcess(string pid)
    {
        ProcessHandler.Instance.SetProcessIndex(int.Parse(processGroup.ToString() + pid));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
