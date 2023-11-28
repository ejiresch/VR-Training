using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GrabTask : Task
{
    public GameObject grabObject;
    [HideInInspector] [SerializeField] public bool[] values = new bool[100];
    // Wird beim Start der Task aufgerufen
    public override void StartTask()
    {

        grabObject = base.FindTool(grabObject.name);
        /*
        pressObject.GetComponent<PressObject>().SetPressable(true);
        Type myType = pressObject.GetComponents(typeof(PressObject))[0].GetType();
        IList<FieldInfo> props = new List<FieldInfo>(myType.GetFields());
        int i = 0;
        foreach (FieldInfo prop in props)
        {
            if (prop.Name == "pressable") continue;
            if (prop.GetValue(pressObject.GetComponents(typeof(PressObject))[0]) is bool)
            {
                prop.SetValue(pressObject.GetComponents(typeof(PressObject))[0], values[i]);
                i++;
            }

        }
        */
        base.StartTask();
    }

    
    protected override void CompReset()
    {
        //pressObject.GetComponent<PressObject>().SetTaskFinished(false);
    }
    

    protected override IEnumerator TaskRunActive()
    {
        while (!grabObject.GetComponent<InteractableObject>().GetIsGrabbed())
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }
}


/// <summary>
/// Klasse um die Einstellungsm�glichkeiten eines PressObjects je nach Task einzustellen. 
/// <remark><br><b>Autor:</b> Marvin Fornezzi</br></remark>
/// </summary>
#if UNITY_STANDALONE_WIN
[CustomEditor(typeof(GrabTask))]
public class CustomInspectorGrabTask : Editor
{
    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var task = target as PressTask;
        Component[] comp = task.pressObject.GetComponents(typeof(InteractableObject));
        if (task.pressObject == null) return;
        GUILayout.Space(20f);
        GUILayout.Label("InteractableOject options");
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
