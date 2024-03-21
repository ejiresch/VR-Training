using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

/**
 * Implementiert nach dem Tutorial von https://www.youtube.com/watch?v=9LwOmMzOrp4
 * Passt die Progress Bar der Videoleiste automatisch an die Laenge des abgespielten Videos an
 */
public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private VideoPlayer videoplayer;

    private Image progress;

    private Camera cam;

    private void Awake()
    {
        progress = GetComponent<Image>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        //Berechnet die Füllung bzw. den Fortschritt der Progress Bar 
        if(videoplayer.frameCount > 0)
        {
            progress.fillAmount = (float)videoplayer.frame / (float)videoplayer.frameCount;
        }
    }

    /**
     * Beim Ziehen entlang der Videoleiste wird entsprechend gespult.
     * 
     * @param eventData Die Daten des Pointer-Ereignisses.
     */
    public void OnDrag(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    /**
    * Beim Klicken auf die Videoleiste wird an die entsprechende Stelle gespult.
    * 
    * @param eventData Die Daten des Pointer-Ereignisses.
    */
    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    /**
    * Versucht, an die entsprechende Stelle im Video zu springen, basierend auf den Pointer-Ereignisdaten.
    * 
    * @param eventData Die Daten des Pointer-Ereignisses.
    */
    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        // Konvertiert die Bildschirmkoordinaten des Pointer-Ereignisses in lokale Koordinaten der Fortschrittsleiste.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, cam, out localPoint))
        {
            // Berechnet den Prozentsatz der Fortschrittsleiste, an dem geklickt wurde.
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            // Springt zu dem Prozentsatz im Video.
            SkipToPercent(pct);
        }
    }

    /**
    * Überspringt die Wiedergabe des Videos auf einen bestimmten Prozentsatz.
    * 
    * @param pct Der Prozentsatz, zu dem die Wiedergabe übersprungen werden soll.
    */
    private void SkipToPercent(float pct)
    {
        var frame = videoplayer.frameCount * pct;
        videoplayer.frame = (long) frame;
    }
}
