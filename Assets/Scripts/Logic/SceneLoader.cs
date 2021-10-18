using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Loads needed Processes and Assets into the Scene
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ProcessScriptableObject[] processes;
    private GameObject[] toolList;
    private ProcessScriptableObject selectedProcess;
    // Uses the pid to load a certain Process
    public void LoadProcess(string pid)
    {
        foreach(ProcessScriptableObject p in processes)
        {
            if (p.name.Contains(pid)) selectedProcess = p;
            break;
        }
        toolList = selectedProcess.toolList;
        Transform[] spawnPoints = ProcessHandler.Instance.GetSpawnPoints();

        if(toolList.Length >= spawnPoints.Length)
        {
            Debug.LogError("Zu viele Tools, :(, die werdên nicht gespawned");
            return;
        }

        int i = 1;
        foreach (GameObject p in toolList)
        {
            Instantiate(p, spawnPoints[i].position , Quaternion.identity, this.gameObject.transform);
            i++;
        }
    }
    public List<GameObject> GetTaskList()
    {
        return selectedProcess.GetTasklist();
    }
}
