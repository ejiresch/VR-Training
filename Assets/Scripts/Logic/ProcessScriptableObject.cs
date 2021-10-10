using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Scriptable Object Template, contains needed assets etc
[CreateAssetMenu(fileName = "ProcessScriptableObject", menuName = "ScriptableObjects/Process", order = 1)]
public class ProcessScriptableObject : ScriptableObject
{
    public string pName;
    public string description;
    public GameObject[] toolList;
    //TODO: 

    [SerializeField] private GameObject[] taskList;

    /**
     * Returns the remaining Task amount
     */
    public int RemainingTasks()
    {
        return taskList.Length;
    }

    /**
     * Returns Tasklist as a List Object
     */
    public List<GameObject> GetTasklist()
    {
        return new List<GameObject>(taskList);
    }
}
