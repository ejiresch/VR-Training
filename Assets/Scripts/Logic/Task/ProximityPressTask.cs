﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using development_a;
using UnityEditor;
using UnityEngine;

public class ProximityPressTask : PressTask
{
    public float distance = 1.0f;
    private bool isactive = false;
    GameObject[] ProxObjects = new GameObject[2];
    private bool taskActive;
    // Start is called before the first frame update
    void Start()
    {   
        isactive = true;
    }
    public override void StartTask()
    {
        base.StartTask();
        taskActive = true;
        isactive = true;
        
    }
    protected override IEnumerator TaskRunActive()
    {
        isactive = true;
        while (isactive)
        {
            GameObject[] touchObjects = GameObject.FindGameObjectsWithTag("touchHand");
            ProxObjects = touchObjects;
            if (touchObjects != null && touchObjects.Length > 1)
            {
                foreach (GameObject touchObject in touchObjects)
                {
                    touchObject.GetComponent<ProximityActionObject>().SetTouchTarget(pressObject);
                }
                isactive = false;
                taskActive = true;
            }
            yield return new WaitForFixedUpdate();
        }
        while (taskActive)
        {
            pressObject.GetComponent<InteractableHandler>().SetIsGrabbed(Vector3.Distance(pressObject.transform.position, this.transform.position) < distance);
            pressObject.GetComponent<PressObject>().SetIsGrabbed(ProxObjects[0].GetComponent<ProximityActionObject>().GetInRange() || ProxObjects[1].GetComponent<ProximityActionObject>().GetInRange());
            if (pressObject.GetComponent<PressObject>().GetTaskCompletion())
            {
                pressObject.GetComponent<PressObject>().SetTaskFinished(false);
                pressObject.GetComponent<PressObject>().SetIsGrabbed(false);
                EndTask();
                taskActive = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    protected override void EndTask()
    {
        FindObjectOfType<SoundManager>().ManageSound("Blop", true, 1f);
        base.EndTask();
    }
}
/// <summary>
/// Klasse um die Einstellungsmöglichkeiten eines PressObjects je nach Task einzustellen. 
/// <remark><br><b>Autor:</b> Marvin Fornezzi</br></remark>
/// </summary>
#if UNITY_STANDALONE_WIN
[CustomEditor(typeof(ProximityPressTask))]
public class CustomInspectorPressTask : Editor
{
    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var task = target as PressTask;
        Component[] comp = task.pressObject.GetComponents(typeof(PressObject));
        if (task.pressObject == null) return;
        GUILayout.Space(20f);
        GUILayout.Label("PressOject options");
        foreach (Component comp2 in comp)
        {
            Type myType = comp2.GetType();
            IList<FieldInfo> props = new List<FieldInfo>(myType.GetFields());
            int i = 0;
            foreach (FieldInfo prop in props)
            {
                if (prop.Name == "pressable") continue;
                if (prop.GetValue(comp[0]) is bool)
                {
                    task.values[i] = GUILayout.Toggle(task.values[i], prop.Name);
                    i++;
                }

            }
        }
    }
}
#endif
