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

        foreach (GameObject p in toolList)
        {
            Instantiate(p, new Vector3(Random.Range(-8f, 8f), 0, 0) , Quaternion.identity, this.gameObject.transform);
        }
    }
    public List<GameObject> GetTaskList()
    {
        return selectedProcess.GetTasklist();
    }
}
