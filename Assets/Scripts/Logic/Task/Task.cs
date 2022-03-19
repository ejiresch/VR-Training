using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Abstract class that Describes a Task
public abstract class Task : MonoBehaviour
{
    public string tName;
    public string description;
    public GameObject[] spawnedTools;
    public bool warningMessage_BeideHaende = false;
    public bool warningMessage_KanueleFesthalten = false;
    // Gets called when Task is started
    public virtual void StartTask()
    {
        if(warningMessage_BeideHaende) ProcessHandler.Instance.ShowWarning(0);
        if(warningMessage_KanueleFesthalten) ProcessHandler.Instance.ShowWarning(1);
    }
    public void SetSpawnTools(GameObject[] toolList) => spawnedTools = toolList;
    // Checks if Task is successful
    public virtual bool IsSuccessful(CollisionEvent ce) => false;
    public virtual bool IsSuccessful(RotationCollisionEvent ce) => false;
    public GameObject FindTool(string prefabName)
    {
        for (int i = 0; i < spawnedTools.Length; i++)
        {
            if ((prefabName + "(Clone)") == spawnedTools[i].name) return spawnedTools[i];
        }
        GameObject tco = ProcessHandler.Instance.GetCompoundObject();
        foreach (Rigidbody child in tco.GetComponentsInChildren<Rigidbody>())
        {
            Debug.Log("Prefab: "+prefabName);
            Debug.Log("Child: "+child.gameObject.name);
            if (child.gameObject.name == prefabName) return child.gameObject;
        }
        return null;
    }
    public virtual void FinishTask() => Destroy(this);
}
