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
    public GameObject compoundOb;
    [Tooltip("Patienten Modell welches für den Prozess verwendet werden soll.")]
    public GameObject patientModel; 
    [SerializeField] private GameObject[] taskList;
    public int nextProcess;

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
    public GameObject GetCompound()
    {
        if (compoundOb != null)
            return compoundOb;
        return null;
    }
    public GameObject GetPatientModel()
    {
        if(patientModel != null)
            return patientModel;
        return null;
    }
    /*
     * nach beendung von task wird direkt nächster task gestartet
     * 
     */
    public int GetNextTask()
    {
        return this.nextProcess;
    }

}
