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
    public bool resetToolOnCompletion = false;

    // Gets called when Task is started
    public virtual void StartTask()
    {
        if(warningMessage_BeideHaende) ProcessHandler.Instance.ShowWarning(0);
        if(warningMessage_KanueleFesthalten) ProcessHandler.Instance.ShowWarning(1);
        if (resetToolOnCompletion) PlayerPrefs.SetInt("resetCommand", resetToolOnCompletion ? 1:0) ;
    }
    // Setzt die Liste an erzeugten Tools, welche gesucht werden kann
    public void SetSpawnTools(GameObject[] toolList) => spawnedTools = toolList;

    // Findet ein Tool in der Liste, anhand eines Namens 
    /// <summary>
    /// Sucht nach einem Tool in der Liste an spawnedTools
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public GameObject FindTool(string prefabName)
    {
        for (int i = 0; i < spawnedTools.Length; i++)
        {
            if ((prefabName + "(Clone)") == spawnedTools[i].name) return spawnedTools[i];
        }
        GameObject tco = ProcessHandler.Instance.GetCompoundObject();
        foreach (Rigidbody child in tco.GetComponentsInChildren<Rigidbody>())
        {
            if (child.gameObject.name == prefabName) return child.gameObject;
        }
        return null;
    }
    // Wird am Ende der Task aufgerufen
    public virtual void FinishTask() => Destroy(this.gameObject);

    //Created by Simon
    public virtual List<GameObject> HighlightedObjects()
    {
        return null;
    }
    ///<summary>
    ///  Sucht ein GameObject aus den spawnedTools anhand des Namens, wobei das GameObject auch ein child sein kann. 
    /// </summary>
    /// <param name="prefabName"> Name des zu suchenden <see cref="GameObject"/></paramref>
    /// 
    public GameObject FindToolAsChild(string prefabName, GameObject currentGameObject)
    {
        foreach(Transform child in currentGameObject.transform)
        {   
            if (currentGameObject.transform.childCount > 0)
            {
                FindToolAsChild(prefabName, child.gameObject);
            }
            if (currentGameObject.name == prefabName) return currentGameObject;
        }
        return currentGameObject;
    }
}
