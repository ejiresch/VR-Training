using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public List<MonoBehaviour> resetTargetList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetComp()
    {
        try
        {
            foreach (Task task in resetTargetList)
            {
                task.ResetTool();
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Fehler in Reset Methode des Tasks: " +e.Source);
        }
    }
    public void Register(MonoBehaviour toolToReset)
    {
        resetTargetList.Add(toolToReset);
    }
}
