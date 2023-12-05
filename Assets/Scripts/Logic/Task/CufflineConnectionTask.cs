using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

// Eine Connection Task, welche spezifisch fuer die Verbindung mit der Cuffline zustaendig ist
public class CufflineConnectionTask : PressTask
{
    public GameObject connector;
    private GameObject connectible;
    // Wird beim Start der Task ausgefuehrt
    public override void StartTask()
    {
        base.StartTask();
        connector = base.FindTool(connector.name);
        connectible = GameObject.FindGameObjectWithTag("CompoundGrabbablePart");
        if (connector != null && connectible != null)
        {
            
            connector.GetComponent<ConnectorObject>().connectorActive = true;
            connectible.GetComponent<Connectible>().SetConnector(connector.GetComponent<ConnectorObject>());
            connectible.GetComponent<InteractableObject>().SetGrabbable(true);
        }
    }

    protected override void CompReset()
    {
        connector.GetComponent<PressObject>().SetTaskFinished(false);
        connector.GetComponent<ConnectorObject>().SetTaskFinished(false);
        try
        {
            GameObject temp = GameObject.FindGameObjectWithTag("CompoundGrabbablePart").transform.parent.parent.gameObject;
            if (temp = null)
            {
                temp.GetComponent<InteractableObject>().SetGrabbable(true);
                temp.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        catch
        {

        }
        
    }

    protected override IEnumerator TaskRunActive()
    {
        connector.GetComponent<PressObject>().SetPressable(connector.GetComponent<ConnectorObject>().GetIsGrabbed());

        while (!connector.GetComponent<PressObject>().GetTaskCompletion())
        {
            connector.GetComponent<ConnectorObject>().SetTaskFinished(true);
            connector.GetComponent<PressObject>().SetPressable(connector.GetComponent<ConnectorObject>().GetIsGrabbed());
            yield return new WaitForFixedUpdate();
        }
        EndTask();
        
    }

}
/// <summary>
/// Klasse um die Einstellungsmöglichkeiten eines PressObjects je nach Task einzustellen. 
/// <remark><br><b>Autor:</b> Marvin Fornezzi</br></remark>
/// </summary>
#if UNITY_STANDALONE_WIN
[CustomEditor(typeof(CufflineConnectionTask))]
public class CustomInspector : Editor
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