using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Loads needed Processes and Assets into the Scene
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ProcessScriptableObject[] processes;
    public List<GameObject> toolList = new List<GameObject>();
    public ProcessScriptableObject selectedProcess;
    public GameObject placeholderTool;
    // Uses the pid to load a certain Process
    public void LoadProcess(int pid)
    {

        foreach(ProcessScriptableObject p in processes)
        {
            string[] temp = p.name.Split('_');
            if (int.Parse(temp[1] + temp[2]) == pid)
            {
                selectedProcess = p;
                break;
            }
        }
        
        GameObject[] tools = selectedProcess.toolList;
        Transform patientSpawn = ProcessHandler.Instance.GetPatientSpawn();
        GameObject com = selectedProcess.GetCompound();
        GameObject patientModel = Instantiate(selectedProcess.GetPatientModel(),patientSpawn);
        if (com != null)
        {
            GameObject inst = Instantiate(com);
            patientModel.GetComponent<ConnectorObject>().ForceConnect(inst);
            ProcessHandler.Instance.SetCompoundOb(inst);
            toolList.Add(inst);
        }
        toolList.Add(patientModel);
        

        Transform[] spawnPoints = ProcessHandler.Instance.GetSpawnPoints();
        if(tools.Length >= spawnPoints.Length)
        {
            return;
        }

        int i = 1;

        foreach (GameObject p in tools)
        {
            if (p == null)
            {
                
                toolList.Add(Instantiate(placeholderTool, spawnPoints[i].position, Quaternion.identity, this.gameObject.transform));
                i++;
                continue;
            }

            /*
             * für spawnBack
             * übergibt gewählten SpawnPoint
             */
            GameObject currentTool = Instantiate(p, spawnPoints[i].position, p.transform.localRotation, this.gameObject.transform);

            if (currentTool.GetComponent<spawnBack>() != null)
            { 
                currentTool.GetComponent<spawnBack>().spawnPoint = spawnPoints[i];
            }
            toolList.Add(currentTool);


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
    //
    public int GetFollowupTask()
    {
        return selectedProcess.GetNextTask();
    }
}
