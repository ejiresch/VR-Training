using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/* Bezieht sich auf alle Darstellungen und die Logik im Bereich HUD */
public class HUDHandler : MonoBehaviour
{
    public TextMeshProUGUI hud_text;
    //The image of the arrow
    public RawImage tutorialArrow;
    //das Transform des Pfeils um ihn zu drehen
    public RectTransform tutorialArrowTransform;
    //the trasnform of the camera to know in which direction it is looking
    public Transform camera;
    //Transform der Objekte auf die der Pfeil zeigen muss
    public Transform lookDirection;

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
        float cameraYRotation = camera.rotation.eulerAngles.y;
        Debug.Log(cameraYRotation);
        //if the camera is looking at the right spot, no arrow is visable
        if(cameraYRotation < 300 && cameraYRotation > 260)
        {
            tutorialArrow.enabled = false;
        }
        else 
        {
            tutorialArrow.enabled = true;
            //tutorialArrow_transform.rotation = Quaternion.Euler(0, 0, 180);
            tutorialArrowTransform.LookAt(lookDirection);
            tutorialArrowTransform.Rotate(0, 270, 0);
        }
    }
}
