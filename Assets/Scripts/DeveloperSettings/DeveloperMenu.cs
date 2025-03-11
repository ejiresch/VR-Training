using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/**
 * Diese Klasse verwaltet welche Buttons im Developer Mode active sind 
 * und wechselt passend die Farbe
 */
public class DeveloperMenu : MonoBehaviour
{
    public int button;
    private HandAnimation handAnimation;
    public Color highlightedColor = new Color(0.3f, 1, 0.3f);
    public Boolean isHandanimationSelected=false, isHighlightingSelected = false, isVibrationSelected = false, isFPSCounterSelected = false;
    private GameObject fpsCounter;
    public Button[] optionButtons;

    // Start is called before the first frame update
    void Start()
    {
        fpsCounter = FindObjectOfType<FPSCounter>().gameObject;
        fpsCounter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E)){
        //    fpsCounter.SetActive(!fpsCounter.active);
        //}
    }
    /*
     * Je nachdem welche Button angeklickt wurde wird die Farbe geändert und gespeichert ob er gerade active ist
     */
    public void optionSelected(int pid)
    {
        this.button = pid;
        if (this.button == 1)
        {
            if (isHandanimationSelected == false)
            {
                isHandanimationSelected = true;
                ButtonActive(0);
                UnityEngine.Debug.Log("handbutton an");
            }
            else
            {
                isHandanimationSelected = false;
                ButtonInactive(0);
                UnityEngine.Debug.Log("Handbutton aus");
            }
        }
        else if (this.button == 2)
        {
            
            if (isVibrationSelected == false)
            {
                isVibrationSelected = true;
                ButtonActive(1);
                UnityEngine.Debug.Log("Vibration an");
            }
            else
            {
                isVibrationSelected = false;
                ButtonInactive(1);
                UnityEngine.Debug.Log("Vibration aus");
            }
        }
        else if (this.button == 3)
        {
            if (isHighlightingSelected == false)
            {
                isHighlightingSelected = true;
                ButtonActive(2);
                UnityEngine.Debug.Log("Highlighting an");
                
            }
            else
            {
                isHighlightingSelected = false;
                ButtonInactive(2);
                UnityEngine.Debug.Log("Highlighting aus");
            }
        } else if(this.button == 4)
        {
            if(isFPSCounterSelected == false)
            {
                isFPSCounterSelected = true;
                ButtonActive(3);
                fpsCounter.SetActive(true);
                UnityEngine.Debug.Log("FPS-Counter an");
            } else
            {
                isFPSCounterSelected = false;
                ButtonInactive(3);
                fpsCounter.SetActive(false);
                UnityEngine.Debug.Log("FPS-Counter aus");
            }
        }
    }
    //Setzt die Farbe der Buttons auf Highlighted
    public void ButtonActive(int button)
    {
        var colors = optionButtons[button].colors;
        colors.normalColor = highlightedColor;
        colors.pressedColor = highlightedColor;
        colors.selectedColor = highlightedColor;
        colors.highlightedColor = highlightedColor;
        this.optionButtons[button].colors = colors;
    }
    //Setzt die Farbe der Buttons auf normal
    public void ButtonInactive(int button)
    {
        var colors = optionButtons[button].colors;
        colors.normalColor = Color.white;
        colors.pressedColor = Color.white;
        colors.selectedColor = Color.white;
        colors.highlightedColor = Color.white;
        this.optionButtons[button].colors = colors;
    }

 
}

