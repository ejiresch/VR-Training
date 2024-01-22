using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string pid = "0";
    private HandAnimation handAnimation;
    public Color highlightedColor = new Color(0.3f, 1, 0.3f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void optionSelected(string pid)
    {
        this.pid = pid;
        if(this.pid == "1") {
            handAnimation.staticHands();
            var colors = GameObject.Find("Handanimation").GetComponent<Button>().colors;
            colors.normalColor = highlightedColor;
            colors.pressedColor = highlightedColor;
            colors.selectedColor = highlightedColor;
            colors.highlightedColor = highlightedColor;
            GameObject.Find("Handanimation").GetComponent<Button>().colors = colors;

        }
        else if(this.pid == "2")
        {

            
        }
        else if(this.pid== "3")
        {

        
        }
    }
}

/**
 * var colors = GameObject.Find("Handanimation").GetComponent<Button>().colors;
            colors.normalColor = highlightedColor;
            colors.pressedColor = highlightedColor;
            colors.selectedColor = highlightedColor;
            colors.highlightedColor = highlightedColor;
            GameObject.Find("Handanimation").GetComponent<Button>().colors = colors;
 */
