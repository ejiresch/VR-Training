using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/**
 * Hier stehen alle Methoden (für Buttons und Sonstiges), die das MainMenu braucht.
 */
public class MenuManager : MonoBehaviour
{
    /* Methode für Button "Start" */
    public void OpenModeOptions()
    {
        Debug.Log("test");
    }
    public void StartSimulation(int whatMode)
    {
        // Will open the Main Scene
        SceneManager.LoadScene("[NameOfScene]");
    }

    /* Methode für Button "Exit" */
    public void OpenWarningPanel()
    {
        // Will fog the Main menu and 2 Buttons with "yes" and "no" will appear

    }
    public void WarningMessage(bool answer)
    {
        if(answer == true)
        {
            Application.Quit();
        }
        else
        {
            //Back to the Main Menu
        }
    }
    

}
