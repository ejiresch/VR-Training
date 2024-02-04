using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class that Describes a Class
/// If a new task is implemented, this class has to be derived of.
/// <remark><br><b>Some methodes have to be overridden</b></br></remark>
/// </summary>
public abstract class Task : MonoBehaviour
{
    public string tName;
    [Tooltip("Die Taskbeschreibung die am Whiteboard steht.")]
    public string description;
    [HideInInspector] public GameObject[] spawnedTools;
    public bool warningMessage_BeideHaende = false;
    public bool warningMessage_KanueleFesthalten = false;
    public bool resetToolOnCompletion = false;

    private GameObject rightObject = null;

    protected Canvas tutorialCanvas;
    protected Canvas tutorialBackgroundCanvas;
    protected HUDHandler hudHandler;

    private Canvas canvas;
    private Boolean active = false;
    private MeshRenderer image;

    /// <summary>
    /// Gets called when Task is started
    /// </summary>
    public virtual void StartTask()
    {
        canvasSetup();
        clearCanvas();
        if (warningMessage_BeideHaende) ProcessHandler.Instance.ShowWarning(0);
        if (warningMessage_KanueleFesthalten) ProcessHandler.Instance.ShowWarning(1);
        if (resetToolOnCompletion) PlayerPrefs.SetInt("resetCommand", resetToolOnCompletion ? 1 : 0);
        StartCoroutine(TaskRunActive());
    }

    public virtual void StartTask(bool canvasClear)
    {
        canvasSetup();
        if (canvasClear)
            clearCanvas();

        if (warningMessage_BeideHaende) ProcessHandler.Instance.ShowWarning(0);
        if (warningMessage_KanueleFesthalten) ProcessHandler.Instance.ShowWarning(1);
        if (resetToolOnCompletion) PlayerPrefs.SetInt("resetCommand", resetToolOnCompletion ? 1 : 0);
        StartCoroutine(TaskRunActive());
    }

    /// <summary>
    /// Setzt die Liste an erzeugten Tools, welche gesucht werden können
    /// </summary>
    /// <param name="toolList">Tool Liste</param>
    public void SetSpawnTools(GameObject[] toolList) => spawnedTools = toolList;

    /// <summary>
    /// Findet ein Tool in der Liste, anhand eines Namens
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public GameObject FindTool(string prefabName)
    {

        for (int i = 0; i < spawnedTools.Length; i++)
        {
            if ((prefabName + "(Clone)") == spawnedTools[i].name) return spawnedTools[i];

        }
        GameObject tco = ProcessHandler.Instance.GetCompoundObject();
        GameObject foundTool = FindToolAsChild(prefabName, tco);

        if (foundTool != null)
        {
            return foundTool;
        }
        foreach (Rigidbody child in tco.GetComponentsInChildren<Rigidbody>())
        {
            if (child.gameObject.name == prefabName) return child.gameObject;
        }

        return null;
    }
    // Wird am Ende der Task aufgerufen
    public virtual void FinishTask() => Destroy(this.gameObject);

    //Created by Simon
    public virtual List<GameObject> HighlightedObjects()
    {
        return null;
    }
    ///<summary>
    ///  Sucht ein GameObject aus den spawnedTools anhand des Namens, wobei das GameObject auch ein child sein kann. 
    /// </summary>
    /// <param name="prefabName"> Name des zu suchenden <see cref="GameObject"/></paramref>
    ///<returns></returns>
    public GameObject FindToolAsChild(string prefabName, GameObject currentGameObject)
    {
        foreach (Transform child in currentGameObject.transform)
        {
            if (currentGameObject.name == prefabName)
            {
                rightObject = currentGameObject;
                return rightObject;
            }
            if (currentGameObject.transform.childCount > 0)
            {
                FindToolAsChild(prefabName, child.gameObject);
            }
        }
        return rightObject;
    }
    /// <summary>
    /// Beendet einen Task und startet einen neuen Task durch den ProcessHandler
    /// <br><b>Autor: </b>Marvin Fornezzi</br>
    /// </remark>
    /// </summary>
    protected virtual void EndTask()
    {
        CompReset();
        ProcessHandler.Instance.NextTask();
    }
    /// <summary>
    /// In ComReset soll der taskFinished Status der verwendeten Objekte zurückgesetzt werden
    ///   
    /// </summary>
    /// <example>
    /// Der Code könnte wie folgt aussehen. Das GameObject muss dabei von InteractableObject erben.
    /// <code>
    /// GameObject.SetTaskFinished(false);
    /// </code>
    /// </example>
    protected abstract void CompReset();
    /// <summary>
    /// TaskRunAktive wird aufgerufen wenn ein Task gestartet wird. In dem code der Methde soll die Überprüfung statfinden, ob die Taskbedingungen erfüllt wurden.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator TaskRunActive();
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EndTask();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (active == false)
            {
                active = true;
                canvas = GameObject.Find("ButtonsOption").GetComponent<Canvas>();
                canvas.enabled = true;

                image = GameObject.Find("HelpPictureOptions").GetComponent<MeshRenderer>();
                image.enabled = false;
            }
            else
            {
                active = false;
                canvas = GameObject.Find("ButtonsOption").GetComponent<Canvas>();
                canvas.enabled = false;

                image = GameObject.Find("HelpPictureOptions").GetComponent<MeshRenderer>();
                image.enabled = true;

            }

        }
    }

    /// <summary>
    /// findet den Tutorial Canvas und den HUD Handler
    /// </summary>
    protected void canvasSetup()
    {
        tutorialCanvas = GameObject.Find("TutorialCanvas").GetComponent<Canvas>();
        tutorialBackgroundCanvas = GameObject.Find("TutorialBackgroundCanvas").GetComponent<Canvas>();
        hudHandler = GameObject.Find("HUDHandler").GetComponent<HUDHandler>();
    }

    /// <summary>
    /// setzt den Tutorial Canvas und HUD Handler auf Anfang züruck
    /// </summary>
    protected void clearCanvas()
    {
        hudHandler.setTutorialMode(false);
        tutorialCanvas.enabled = false;
        tutorialBackgroundCanvas.enabled = false;
        Button button = tutorialCanvas.GetComponentInChildren<Button>();

        if (button != null)
        {
            button.enabled = true;
            button.GetComponent<Image>().enabled = true;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
    }
}

