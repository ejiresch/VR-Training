using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ButtonHover : MonoBehaviour
{

    public float timeViewed;
    public string lastHit;
    public Image[] images;
    private Animator anim = null;
    //public WhiteboardHandler whiteboard;

    public void Start()
    {
        timeViewed = 0;
        lastHit = null;
    }
    public void Update()
    {
        int layerMask = LayerMask.GetMask("ViewSelect");
        RaycastHit hit;
        var main = Camera.main.transform;
        bool success= Physics.Raycast(main.position, main.forward, out hit,layerMask);
        float indicatorTimer = 0;
        bool reset = false;
       

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

            switch (lastHit)
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
                case "entfernen":
                    images[3].fillAmount += indicatorTimer / 5;
                    break;
                case "verlassen":
                    images[4].fillAmount += indicatorTimer / 5;
                    break;
                case "Tutorial":
                    images[5].fillAmount += indicatorTimer / 5;
                    break;

            }

           

            

            if (anim != null)
            {
                anim.SetBool("Open", true);
            }
            if (timeViewed > 5)
            {
                switch (lastHit){
                    case "PEGNahrung":
                        Debug.Log("PEGNahrung");
                       // whiteboard.StartProcess("03");
                        break;
                    case "PEGsonde":
                        Debug.Log("PEGsonde");
                      //  whiteboard.StartProcess("04");
                        break;
                    case "einsetzen":
                        Debug.Log("einsetzen");
                       // whiteboard.StartProcess("02");
                        break;
                    case "entfernen":
                        Debug.Log("entfernen");
                       // whiteboard.StartProcess("01");
                        break;
                    case "verlassen":
                        Debug.Log("verlassen");
                        Application.Quit();
                        break;
                    case "Tutorial":
                        Debug.Log("tutorial");
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
                anim.SetBool("Open", false); //löst NullReferenceException aus aber egal
                reset = false;
            }
            for (int i = 0; i<images.Length; i++)
            {
                images[i].fillAmount = 0;
            }
            
        }
        


    }
}
    
