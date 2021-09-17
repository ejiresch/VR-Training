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
    // public TaskList taskList;
}
