using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Abstract class that Describes a Task
public abstract class Task : MonoBehaviour
{
    public string tName;
    public string description;
    public GameObject[] requiredTools;
    // Gets called when Task is started
    public virtual void StartTask()
    {
    }
    // Checks if Task is successful
    public abstract bool IsSuccessful(CollisionEvent ce);
}
