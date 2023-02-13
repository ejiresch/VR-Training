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
        ProcessHandler.Instance.UINextTask(task.description, isFirst);
        return task;
    }
    // Startet die naechste Task
    public Task StartNextTask()
    {
        GameObject task = Instantiate(taskList[0], this.transform.position, Quaternion.identity, this.transform);
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
        
        /**GameObject one_exists = GameObject.Find("Cube");
        if (one_exists != null)
        {
            Destroy(one_exists);
        }*/


        if (objects != null)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Debug.Log(objects[i]);
                GameObject one = GameObject.Find("Cube");
                one.transform.position = objects[i].transform.position;
                //one.transform.position = new Vector3(objects[i].transform.position.x, 
                //                                    objects[i].transform.position.y, 
                //                                    objects[i].transform.position.z);
                //GameObject.FindGameObjectWithTag("Respawn").transform.position;
                //GameObject.Find("Spot Light").transform.position;
                Debug.Log(one.transform.position);
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
}
