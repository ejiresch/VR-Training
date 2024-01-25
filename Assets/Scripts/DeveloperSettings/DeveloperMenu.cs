using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public int button;
    private HandAnimation handAnimation;
    public Color highlightedColor = new Color(0.3f, 1, 0.3f);

    public Boolean isButtonPressed = false;

    public Boolean isHandanimationSelected, isHighlightingSelected, isVibrationSelected;

    public Button[] optionButtons;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void optionSelected(int pid)
    {
        this.button = pid;
        if (this.button == 1)
        {
            
            if (isHandanimationSelected == false)
            {
                isHandanimationSelected = true;
                var colors = optionButtons[0].colors;
                colors.normalColor = highlightedColor;
                colors.pressedColor = highlightedColor;
                colors.selectedColor = highlightedColor;
                colors.highlightedColor = highlightedColor;
                this.optionButtons[0].colors = colors;

                UnityEngine.Debug.Log("handbutton an");
               
            }
            else
            {

                isHandanimationSelected = false;

                var colors = optionButtons[0].colors;
                colors.normalColor = Color.white; ;
                colors.pressedColor = Color.white; ;
                colors.selectedColor = Color.white; ;
                colors.highlightedColor = Color.white; ;
                this.optionButtons[0].colors = colors;

                UnityEngine.Debug.Log("Handbutton aus");
            }
           

        }
        else if (this.button == 2)
        {
            
            if (isVibrationSelected == false)
            {
                isVibrationSelected = true;
                var colors = optionButtons[1].colors;
                colors.normalColor = highlightedColor;
                colors.pressedColor = highlightedColor;
                colors.selectedColor = highlightedColor;
                colors.highlightedColor = highlightedColor;
                this.optionButtons[1].colors = colors;

                UnityEngine.Debug.Log("Vibration an");
                
            }
            else
            {
                isVibrationSelected = false;
                var colors = optionButtons[1].colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.white;
                colors.selectedColor = Color.white;
                colors.highlightedColor = Color.white;
                this.optionButtons[1].colors = colors;

                UnityEngine.Debug.Log("Vibration aus");
            }

        }
        else if (this.button == 3)
        {
            
            if (isHighlightingSelected == false)
            {
                isHighlightingSelected = true;
                var colors = optionButtons[2].colors;
                colors.normalColor = highlightedColor;
                colors.pressedColor = highlightedColor;
                colors.selectedColor = highlightedColor;
                colors.highlightedColor = highlightedColor;
                this.optionButtons[2].colors = colors;

                UnityEngine.Debug.Log("Highlighting an");
                
            }
            else
            {

                isHighlightingSelected = false;
                var colors = optionButtons[2].colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.white;
                colors.selectedColor = Color.white;
                colors.highlightedColor = Color.white;
                this.optionButtons[2].colors = colors;

                UnityEngine.Debug.Log("Highlighting aus");
            }

        }


    }
}

