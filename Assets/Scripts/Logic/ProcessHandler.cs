using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class responsible for loading Assets and assigning Tasks to the Task-Manager 
public class ProcessHandler : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private UserInterfaceManager uiManager;
    [SerializeField] private GameObject spawnPoints;
    private static ProcessHandler _instance;

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
    void Start()
    {
        LoadScene("process_001");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfTasks()
    {
        Debug.Log("Guys, I am logging off");
    }

    public void ReportCollision(CollisionEvent ce)
    {
        Debug.Log(ce.First.name + " " + ce.Second.name);
        taskManager.HandleCollision(ce);
    }

    // LoadScene is used to load a scene with an PID
    void LoadScene(string pid)
    {
        sceneLoader.LoadProcess(pid);
        taskManager.SetTaskList(sceneLoader.GetTaskList());
    }

    public void UINextTask(string desc, bool isFirst)
    {
        uiManager.NewTask(desc, isFirst);
    }

    public Transform[] GetSpawnPoints()
    {
        return spawnPoints.GetComponentsInChildren<Transform>();
    }
}
