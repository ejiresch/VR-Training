using System.Collections;
using System.Collections.Generic;
using development_a;
using UnityEngine;
// Class responsible for managing all Tasks
public class TaskManager : MonoBehaviour
{
    private List<GameObject> taskList;
    public List<GameObject> toolList;
    [SerializeField] private Task currentTask;
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
    public Task StartNextTask()
    {
        GameObject task = Instantiate(taskList[0], this.transform.position, Quaternion.identity, this.transform);
        Task t = task.GetComponent<Task>();
        taskList.RemoveAt(0);
        if (t != null)
        {
            t.SetSpawnTools(toolList.ToArray());
            t.StartTask();
            currentTask = t;
        }
        return t;
    }

    // Gets invoked, when to Interactibles collide
    public void HandleCollision(CollisionEvent ce)
    {
        if (currentTask.IsSuccessful(ce))
        {
            NextTask(false);
        }
    }

    // Sets the TaskList
    public void SetTaskList(List<GameObject> taskList)
    {
        this.taskList = taskList;
        this.currentTask = NextTask(true);
    }
    public void SetToolList(List<GameObject> toolList)
    {
        toolList.Add(GameObject.Find("Woman(Clone)"));
        this.toolList = toolList;
    }

}
