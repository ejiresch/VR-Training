using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public WorldspaceHandler handler;

    public void NewTask(string taskdescription, bool isFirst) 
    {
        if (isFirst) handler.StartTask(taskdescription);
        else handler.NewTask(taskdescription);
    }
}
