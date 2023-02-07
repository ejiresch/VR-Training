using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public List<ResetInterface> resetTargetList = new List<ResetInterface>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTool()
    {
        try
        {
            foreach (ResetInterface task in resetTargetList)
            {
               task.ResetComp();
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Fehler in Reset Methode des Tasks: " +e);
        }
    }
    public void Register(ResetInterface toolToReset)
    {
        resetTargetList.Add(toolToReset);
    }
}
