using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that Describes a Task
/// </summary>
public abstract class Task : MonoBehaviour
{
    public string tName;
    public string description;
    public GameObject[] spawnedTools;
    public bool warningMessage_BeideHaende = false;
    public bool warningMessage_KanueleFesthalten = false;
    public bool resetToolOnCompletion = false;
    private GameObject rightObject = null;

    /// <summary>
    /// Gets called when Task is started
    /// </summary>
    public virtual void StartTask()
    {
        if(warningMessage_BeideHaende) ProcessHandler.Instance.ShowWarning(0);
        if(warningMessage_KanueleFesthalten) ProcessHandler.Instance.ShowWarning(1);
        if (resetToolOnCompletion) PlayerPrefs.SetInt("resetCommand", resetToolOnCompletion ? 1:0) ;
    }
    /// <summary>
    /// Setzt die Liste an erzeugten Tools, welche gesucht werden kann
    /// </summary>
    /// <param name="toolList">Tool Liste</param>
    public void SetSpawnTools(GameObject[] toolList) => spawnedTools = toolList;

    /// <summary>
    /// Findet ein Tool in der Liste, anhand eines Namens
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
        GameObject foundTool = FindToolAsChild(prefabName, tco);
        
        if (foundTool != null)
        {
            return foundTool;
        }
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
    ///<returns></returns>
    public GameObject FindToolAsChild(string prefabName, GameObject currentGameObject)
    {
        foreach(Transform child in currentGameObject.transform)
        {   
            if (currentGameObject.name == prefabName)
            {
                rightObject = currentGameObject;
                return rightObject;
            }
            if (currentGameObject.transform.childCount > 0)
            {
                FindToolAsChild(prefabName, child.gameObject);
            }
        }
        return rightObject;
    }
}
