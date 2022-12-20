using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class processUpdate : MonoBehaviour
{
    public SceneLoader scene;
    public ProcessScriptableObject process;

    //public Button button;
    // Start is called before the first frame update
    void Start()
    {
        //Schaut überall nach dem
        scene = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        //Wenn pName gleich ist wie der Process zu dem das Video gehört, gameObject.enabeld() oder so.  
        //Wenn der Process gleich dem Process ist wird das GameObject von dem es ein Compoment ist deactiviert.
        if (!process.pName.Equals(scene.selectedProcess.pName)){
            gameObject.SetActive(false);
        }
    }

    public bool processUpdatAufruf()
    {
        //GetComponent<SceneLoader>().selectedProcess.pName;
        //GameObject.FindGameObjectsWithTag("Video_8_hinein");
        return true;
    }
}
