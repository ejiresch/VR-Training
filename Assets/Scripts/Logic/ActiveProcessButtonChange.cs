using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveProcessButtonChange : MonoBehaviour
{
    public SceneLoader scene;
    public ProcessScriptableObject process;
    public Color highlightedColor = new Color(0.3f, 1, 0.3f);

    // Start is called before the first frame update
    void Start()
    {
        scene = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (process.pName.Equals(scene.selectedProcess.pName))
        {
            //Debug.Log(process.pName);
            //Debug.Log(scene.selectedProcess.pName);

            var colors = gameObject.GetComponent<Button>().colors;
            colors.normalColor = highlightedColor;
            colors.pressedColor = highlightedColor;
            colors.selectedColor = highlightedColor;
            colors.highlightedColor = highlightedColor;
            this.gameObject.GetComponent<Button>().colors = colors;
        } 
        /**else
        {
            var colors = this.gameObject.GetComponent<Button>().colors;
            colors.normalColor = Color.white;
            colors.pressedColor = Color.white;
            colors.selectedColor = Color.white;
            colors.highlightedColor = Color.white;
            this.gameObject.GetComponent<Button>().colors = colors;
        }*/


    }
}
