using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Bezieht sich auf alle Darstellungen und die Logik im Bereich HUD */
public class HUDHandler : MonoBehaviour
{
    public TextMeshProUGUI hud_text;

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
}
