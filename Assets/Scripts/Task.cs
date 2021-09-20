using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public string tName;
    public string description;
    public GameObject[] requiredTools;
   
    public virtual void StartTask()
    {
    }

    public abstract bool IsSuccessful();
}
