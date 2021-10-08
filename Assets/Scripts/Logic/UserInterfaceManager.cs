using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public CorkboardHandler handler;

    public void NewTask(string taskdescription, bool isFirst) 
    {
        if (isFirst) handler.FirstTask(taskdescription);
        else handler.NewTask(taskdescription);
    }
}
