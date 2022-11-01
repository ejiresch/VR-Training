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
    public void LoadProcess(int pid)
    {

        foreach(ProcessScriptableObject p in processes)
        {
            if (p.name.Contains(pid.ToString()))
            {
                selectedProcess = p;
                break;
            }
        }
        
        GameObject[] tools = selectedProcess.toolList;
        Transform patientSpawn = ProcessHandler.Instance.GetPatientSpawn();
        GameObject com = selectedProcess.GetCompound();
        GameObject woman = Instantiate(selectedProcess.GetWoman(),patientSpawn); 
        if (com != null)
        {
            GameObject inst = Instantiate(com);
            woman.GetComponent<ConnectorObject>().ForceConnect(inst);
            ProcessHandler.Instance.SetCompoundOb(com);
        }
        toolList.Add(woman);
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
