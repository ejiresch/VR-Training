using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
/*
 * Die Klasse schaut ob man im main menu �ber einen Knopf hovered,
 * und l�dt die passende Simulation, einzige Klasse die im main menu verwendet wird
 */
public class ButtonHover : MonoBehaviour
{

    public float timeViewed;
    public string lastHit;
    public Image[] images; //rote border um Kn�pfe sind Bilder die sich auff�llen
    private Animator anim = null;
    public bool reset = false;
    private int processGroup = 1;
    private PlayerPrefs playerPrefs;

    

    public void Start()
    {
        timeViewed = 0;
        lastHit = null;
    }
   /*
    * 
    */
    public void Update()
    {
        int layerMask = LayerMask.GetMask("ViewSelect");
        RaycastHit hit;
        var main = Camera.main.transform;
        bool success= Physics.Raycast(main.position, main.forward, out hit,layerMask);
        float indicatorTimer = 0;

        

        if (success == true)
        {
            indicatorTimer+=Time.deltaTime;
            

            lastHit = hit.collider.gameObject.tag;
            timeViewed+=Time.deltaTime;
            anim = hit.collider.gameObject.GetComponent<Animator>();
            reset = true;

            /*
            if (lastHit == "PEGNahrung")
            {
                images[0].fillAmount += indicatorTimer / 5;
            }
            else if(lastHit == "PEGsonde")
            {
                images[1].fillAmount += indicatorTimer / 5;
            }
            else if(lastHit == "einsetzen")
            {
                images[2].fillAmount += indicatorTimer / 5;
            }
            else if(lastHit == "entfernen")
            {
                images[3].fillAmount += indicatorTimer / 5;
            }
            else if(lastHit == "verlassen")
            {
                images[4].fillAmount += indicatorTimer / 5;
            }
            else if(lastHit == "Tutorial")
            {
                images[5].fillAmount += indicatorTimer / 5;
            }
             */

            switch (lastHit) //Je nachdem welcher Knopf angeschaut wird f�llt sich die rote umrandung auf
            {
                case "PEGNahrung":
                    images[0].fillAmount += indicatorTimer / 5;
                    break;
                case "PEGsonde":
                    images[1].fillAmount += indicatorTimer / 5;
                    break;
                case "einsetzen":
                    images[2].fillAmount += indicatorTimer / 5;
                    break;
                case "verlassen":
                    images[3].fillAmount += indicatorTimer / 5;
                    break;
                case "Tutorial":
                    images[4].fillAmount += indicatorTimer / 5;
                    break;
               

            }

           

            

            if (anim != null)
            {
                anim.SetBool("Open", true);
            }
            if (timeViewed > 5)
            {
                string ppkey = "Process_Index";
                switch (lastHit){
                    case "PEGNahrung": // l�dt die passende simulation
                        //Debug.Log("PEGNahrung");
                        PlayerPrefs.SetInt(ppkey, 103);
                        SceneManager.LoadScene(0);
                        break;

                    case "PEGsonde":
                        //Debug.Log("PEGsonde");
                        PlayerPrefs.SetInt(ppkey, 104);
                        SceneManager.LoadScene(0);
                        break;

                    case "einsetzen":
                        //Debug.Log("einsetzen");
                        PlayerPrefs.SetInt(ppkey, 102);
                        SceneManager.LoadScene(0);
                        break;

                    case "entfernen":
                        //Debug.Log("entfernen");
                        PlayerPrefs.SetInt(ppkey, 101);
                        SceneManager.LoadScene(0);
                        break;

                    case "verlassen":
                        //Debug.Log("verlassen");
                        Application.Quit();
                        break;

                    case "Tutorial":
                        //Debug.Log("tutorial");
                        PlayerPrefs.SetInt(ppkey, 105);
                        SceneManager.LoadScene("PrototypSceneKaraca");
                        break;
                }

            }

        }
        else
        {
            timeViewed = 0;
            lastHit = null;
            if(reset == true)
            {
                anim.SetBool("Open", false); 
                reset = false;
            }
            for (int i = 0; i<images.Length; i++)
            {
                images[i].fillAmount = 0;
            }
            
        }
        


    }
   

}
    
