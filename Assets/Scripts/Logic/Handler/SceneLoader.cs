using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Loads needed Processes and Assets into the Scene
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ProcessScriptableObject[] processes;
    public List<GameObject> toolList = new List<GameObject>();
    private ProcessScriptableObject selectedProcess;
    // Uses the pid to load a certain Process
    public void LoadProcess(string pid)
    {
        foreach(ProcessScriptableObject p in processes)
        {
            if (p.name.Equals(pid))
            {
                selectedProcess = p;
                break;
            }
        }
        GameObject[] tools = selectedProcess.toolList;
        Transform[] spawnPoints = ProcessHandler.Instance.GetSpawnPoints();

        if(tools.Length >= spawnPoints.Length)
        {
            return;
        }

        int i = 1;

        foreach (GameObject p in tools)
        {
            toolList.Add(Instantiate(p, spawnPoints[i].position , Quaternion.identity, this.gameObject.transform));
            i++;
        }
    }
    // Returns all Tasks in a List
    public List<GameObject> GetTaskList()
    {
        return selectedProcess.GetTasklist();
    }
    // Returns all Tools
    public List<GameObject> GetToolList()
    {
        return toolList;
    }
}
