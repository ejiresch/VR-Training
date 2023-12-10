using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/* Bezieht sich auf alle Darstellungen und die Logik im Bereich HUD */
public class HUDHandler : MonoBehaviour
{
    public TextMeshProUGUI hud_text;

    //Atribute, die für den Tutorial-Prozess benötigt werden   
    public GameObject tutorialDirection;    //Das Parent Objekt des Pfeils (ist nur notwendig damit die Animation des Pfeils immer richtig funktioniert)
    public RawImage tutorialArrow;          //The image of the arrow    
    public Transform camera;                //the trasnform of the camera to know in which direction it is looking    
    public Transform lookDirection;         //Transform der Objekte auf die der Pfeil zeigen muss
    private bool tutorialMode;

    public void ShowText() // Darstellung des grünen Textes "Aufgabe abgeschlossen"
    {
        StopAllCoroutines();
        StartCoroutine(Fade_text(0.05f, 0.12f));
    }
    IEnumerator Fade_text(float wait, float value) // Fade Darstellung des Textes
    {   
        while (hud_text.color.a < 1.0f) // Fade in
        {
            hud_text.color = new Color(hud_text.color.r, hud_text.color.g, hud_text.color.b, hud_text.color.a + value);
            yield return new WaitForSeconds(wait);
        }

        yield return new WaitForSeconds(1.3f);

        while (hud_text.color.a > 0.0f) // Fade out
        {
            hud_text.color = new Color(hud_text.color.r, hud_text.color.g, hud_text.color.b, hud_text.color.a - value);
            yield return new WaitForSeconds(wait);
        }
    }

    public void Update()
    {
        if (tutorialMode)
        {
            // Vektor zwischen Kamera und Zielobjekt
            Vector3 toTarget = lookDirection.position - camera.position;

            // Berechne die horizontale Rotation um die Y-Achse
            float angleToTarget = Vector3.SignedAngle(camera.forward, toTarget, Vector3.up);


            //if the camera is looking at the right spot, no arrow is visable
            if (Mathf.Abs(angleToTarget)<30f)
            {
                tutorialArrow.enabled = false;
            }
            else
            {
                tutorialArrow.enabled = true;
                tutorialDirection.transform.LookAt(lookDirection);
                tutorialDirection.transform.Rotate(0, 270, 0);
            }
        }
        else
        {
            tutorialArrow.enabled = false;
        }
    }

    public void setTutorialMode(bool tutorialMode)
    {
        this.tutorialMode = tutorialMode;
    }

    public void setLookDirection(Transform lookDirection)
    {
        this.lookDirection = lookDirection; 
    }
}
