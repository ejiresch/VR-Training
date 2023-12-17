using System.Collections;
using System.Collections.Generic;
using development_a;
using UnityEngine;
// Class responsible for managing all Tasks
public class TaskManager : MonoBehaviour
{
    private List<GameObject> taskList;
    public List<GameObject> toolList;
    private GameObject compoundObject;
    private Task currentTask;
    private GameObject woman;
    public GameObject indicator;
    public List<GameObject> indicatorsCurrent;      //Currently spawned Indicators
    public bool showIndicator = false;
    public Canvas tutorialCanvas;
    public HUDHandler hudHandler;
    /**
     * Returns the next Tasks and removes the Array index
    */
    public Task NextTask(bool isFirst)
    {
        if (taskList.Count <= 0)
        {
            ProcessHandler.Instance.EndOfTasks();
            return null;
        }

        // Sound abspielen, wenn Task abgeschlossen wurde
        gameObject.GetComponent<SoundManager>().ManageSound("taskdone", true, 0);


        Task task = StartNextTask();
        if (task.gameObject.tag == "dontShowWhiteboard")
        {
            ProcessHandler.Instance.UINextTask(task.description, isFirst, false);
        }
        else
        {
            ProcessHandler.Instance.UINextTask(task.description, isFirst);
        }
        return task;
    }
    // Startet die naechste Task
    public Task StartNextTask()
    {
        GameObject task = Instantiate(taskList[0], this.transform.position, Quaternion.identity, this.transform);

        /*
         * für Press Button Task um ihn den Tutorial Canvas und dem HUDHandler mit dem tutorial Pfeil aus der Szene zu übergeben
         * 
         * sollte man in Zukunft wohl ändern, das es im Task selbst mit FindObject oder so geschieht
         * 
         */
        if (task.GetComponent<PressButtonTask>() != null)
        {
            task.GetComponent<PressButtonTask>().tutorialCanvas = tutorialCanvas;
            task.GetComponent<PressButtonTask>().hudHandler = hudHandler;

        }
        if (task.GetComponent<GrabTaskWithCanvas>() != null)
        {
            task.GetComponent<GrabTaskWithCanvas>().tutorialCanvas = tutorialCanvas;
            task.GetComponent<GrabTaskWithCanvas>().hudHandler = hudHandler;

        }

        Task t = task.GetComponent<Task>();
        taskList.RemoveAt(0);
        if (t != null)
        {
            t.SetSpawnTools(toolList.ToArray());
            t.StartTask();
            if(currentTask != null) currentTask.FinishTask();
            currentTask = t;
        }

        List<GameObject> objects = t.HighlightedObjects();
        if (showIndicator == true)
        {
            if (objects != null)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    Debug.Log(objects[i]);
                    GameObject go = Instantiate(indicator);
                    go.transform.position = new Vector3(objects[i].transform.position.x,
                                                        objects[i].transform.position.y + 0.5f,
                                                        objects[i].transform.position.z);
                    Debug.Log(objects[i].transform.position);
                indicatorsCurrent.Add(go);
                }
            }
        }
        return t;
    }

    // Sets the TaskList
    public void SetTaskList(List<GameObject> taskList)
    {
        this.taskList = taskList;
        this.currentTask = NextTask(true);
    }
    // Setzt die Tool-Liste im Task-Manager
    public void SetToolList(List<GameObject> toolList)
    {
        this.toolList = toolList;
        Debug.Log("toolList");
        print("try");
        int i = toolList.Count;
        print(i);
        for(int i2 = 0; i2 < i; i2++)
        {
            print(toolList[i2]);
        }
    }
    public void SetCompoundObject(GameObject compoundObject)
    {
        this.compoundObject = compoundObject;
    }
    public GameObject GetCompoundObject()
    {
        return this.compoundObject;
    }
    public GameObject GetWoman() => this.woman;

    public void SwitchIndicator()
    {
        /**if (showIndicator == true)
        {
            showIndicator = false;
        }
        else if (showIndicator == false)
        {
            showIndicator = true;
        }*/
        showIndicator = !showIndicator;
        Debug.Log("Indicator switched");
        if (showIndicator == false)
        {
            GameObject o;
            for(int i = 0; i < indicatorsCurrent.Count; i++) {
                o = indicatorsCurrent[i];
                o.SetActive(false);
             }
        }

        if (showIndicator == true)
        {
            GameObject o;
            for (int i = 0; i < indicatorsCurrent.Count; i++)
            {
                o = indicatorsCurrent[i];
                o.SetActive(true);
            }
        }
    }
}
