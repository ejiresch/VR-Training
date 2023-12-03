using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class responsible for loading Assets and assigning Tasks to the Task-Manager 
// Also provides References to important GameObjects
public class ProcessHandler : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private UserInterfaceManager uiManager;
    [SerializeField] private GameObject spawnPoints;
    [SerializeField] private GameObject patientSpawn;
    [SerializeField] private Material closePreviewMaterial, farPreviewMaterial;
    private static ProcessHandler _instance;
    public string ppKey = "Process_Index";
    // Singleton Instanz
    public static ProcessHandler Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    // Load one of two processes based on Playerpref
    void Start()
    {
        int pi = 101;
        if (PlayerPrefs.HasKey(ppKey)) pi = PlayerPrefs.GetInt(ppKey);
        else PlayerPrefs.SetInt(ppKey, 0);
        LoadScene(pi);
    }

    // Called when all Tasks are over
    public void EndOfTasks()
    {
        uiManager.EndOfTasks();
    }    
    // Wird beim Anfang der naechsten Task aufgerufen
    // Muss vom aktuellen Task beim Abschluss aufgerufen werden!
    public void NextTask()
    {
        taskManager.NextTask(false);
    }

    // LoadScene is used to load a scene with an PID
    void LoadScene(int pid)
    {
        sceneLoader.LoadProcess(pid);
        taskManager.SetToolList(sceneLoader.GetToolList());
        taskManager.SetTaskList(sceneLoader.GetTaskList());
    }
    // Calls the next tasks
    public void UINextTask(string desc, bool isFirst)
    {
        uiManager.NewTask(desc, isFirst);
    }

    // Calls the next tasks
    public void UINextTask(string desc, bool isFirst,bool showWhiteboard)
    {
        uiManager.NewTask(desc, isFirst, showWhiteboard);
    }

    // Shows a Warning, given by index
    public void ShowWarning(int warningIndex)
    {
        uiManager.ShowWarning(warningIndex);
    }
    // Setzt den Prozessindex in den Player-Index
    public void SetProcessIndex(int i) => PlayerPrefs.SetInt(ppKey, i);
    // Gets all Spawnpoints
    public Transform[] GetSpawnPoints() => spawnPoints.GetComponentsInChildren<Transform>();
    // Getter Methoden
    public Transform GetPatientSpawn() => patientSpawn.transform;
    public TaskManager GetTaskManager() => this.taskManager;
    public Material GetClosePreviewMaterial() => this.closePreviewMaterial;
    public Material GetFarPreviewMaterial() => this.farPreviewMaterial;
    public GameObject GetCompoundObject() => taskManager.GetCompoundObject();
    public string GetPlayerPrefsProcessIndexKey() => ppKey;
    public void SetCompoundOb(GameObject compoundObject)
    {
        taskManager.SetCompoundObject(compoundObject);
    }
    
}
