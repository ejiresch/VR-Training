using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Hier stehen alle Methoden (für Buttons und Sonstiges), die das MainMenu braucht. */
public class MenuManager : MonoBehaviour
{
    public GameObject warning_panel;
    public GameObject main_page;
    public GameObject control_page;


    public void StartSimulation() // Methode für Button "Start" 
    {
        main_page.SetActive(false);
        control_page.SetActive(true);
    }
    public void OpenWarningPanel() // Methode für Button "Exit"
    {
        warning_panel.SetActive(true);
        main_page.SetActive(false);
    }
    public void WarningMessage(bool answer)
    {
        if(answer == true)
        {
            Application.Quit();
        }
        else
        {
            warning_panel.SetActive(false);
            main_page.SetActive(true);
        }
    }
    public void ControllerbelegungButtons(bool answer)
    {
        if (answer == true)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            main_page.SetActive(true);
            control_page.SetActive(false);
        }
    }
}
