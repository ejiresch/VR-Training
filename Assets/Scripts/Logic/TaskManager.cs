using System.Collections;
using System.Collections.Generic;
using development_a;
using UnityEngine;
// Class responsible for managing all Tasks
public class TaskManager : MonoBehaviour
{
    private List<GameObject> taskList;
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
        //gameObject.GetComponent<SoundManager>().ManageSound("taskdone", true, 0);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().ManageSound("taskdone", true, 0);
        
        
        Task task = taskList[0].GetComponent<Task>();
        taskList.RemoveAt(0);
        ProcessHandler.Instance.UINextTask(task.description, isFirst);
        return task;
    }
    // Gets invoked, when to Interactibles collide
    public void HandleCollision(CollisionEvent ce)
    {
        if (currentTask.IsSuccessful(ce))
        {
            Task t = NextTask(false);
            if (t != null) currentTask = t;
        }
    }
    // Gets invoked, when to Interactibles collide
    public void HandleRotationCollision(RotationCollisionEvent ce)
    {
        Debug.Log("dasdasdw");
        if (currentTask.IsSuccessful(ce))
        {
            Task t = NextTask(false);
            if (t != null) currentTask = t;
        }
    }
    // Sets the TaskList
    public void SetTaskList(List<GameObject> taskList)
    {
        this.taskList = taskList;
        this.currentTask = NextTask(true);
    }

}
