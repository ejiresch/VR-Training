using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Hier stehen alle Methoden (für Buttons und Sonstiges), die das MainMenu braucht. */
public class MenuManager : MonoBehaviour
{
    public GameObject warning_panel;
    public GameObject main_page;

    
    public void StartPEGNahrung() 
    {
        SceneManager.LoadScene(1);
    }
    public void StartTracheostomEntfernen()
    {
        SceneManager.LoadScene(1);
    }
    public void StartracheostomEinsetzen()
    {
        SceneManager.LoadScene(1);
    }
    public void StartPEGMedikament()
    {
        SceneManager.LoadScene(1);
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
}
