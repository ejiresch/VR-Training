using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Abstract class that Describes a Task
public abstract class Task : MonoBehaviour
{
    public string tName;
    public string description;
    public GameObject[] requiredTools;
    public GameObject[] spawnedTools;
    // Gets called when Task is started
    public virtual void StartTask()
    {
    }
    public void SetSpawnTools(GameObject[] toolList)
    {
        spawnedTools = toolList;
    }
    // Checks if Task is successful
    public virtual bool IsSuccessful(CollisionEvent ce)
    {
        return false;
    }
    public virtual bool IsSuccessful(RotationCollisionEvent ce)
    {
        return false;
    }
    public GameObject FindTool(string prefabName)
    {
        for (int i = 0; i < spawnedTools.Length; i++)
        {
            if ((prefabName + "(Clone)") == spawnedTools[i].name) return spawnedTools[i];
        }
        return null;
    }
}
