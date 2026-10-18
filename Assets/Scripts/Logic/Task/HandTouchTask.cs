
using development_a;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Implementiert eine Aufgabe, bei der der Benutzer mit seinen Händen
/// ein vorgegebenes Objekt berühren muss.
/// Nach erfolgreichem Abschluss wird ein Handschuh-Sound abgespielt
/// und die Aufgabe beendet.
/// </summary>
public class HandTouchTask : Task
{
    /// <summary>
    /// Das Objekt, das vom Benutzer berührt werden soll.
    /// </summary>
    public GameObject touchTarget;

    /// <summary>
    /// Gibt an, ob die Suche nach den Hand-Objekten aktiv ist.
    /// </summary>
    private bool isactive = false;

    /// <summary>
    /// Speichert die gefundenen GameObjects mit dem Tag "Hand".
    /// </summary>
    private GameObject[] touchObjects;

    /// <summary>
    /// Startet die Aufgabe, sucht das Berührungsziel und aktiviert
    /// die Suche nach den Hand-Objekten.
    /// </summary>
    public override void StartTask()
    {
        // Führt die Startlogik der Basisklasse aus.
        base.StartTask();

        // Ermittelt das Berührungsziel anhand seines Namens.
        touchTarget = base.FindTool(touchTarget.name);

        // Aktiviert die Suche nach den Händen.
        isactive = true;
    }

    /// <summary>
    /// Sucht nach den Hand-Objekten und konfiguriert diese,
    /// sobald mindestens zwei Hände gefunden wurden.
    /// </summary>
    void Update()
    {
        // Führt die Update-Logik der Basisklasse aus.
        base.Update();

        if (isactive)
        {
            // Sucht alle aktiven GameObjects mit dem Tag "Hand".
            touchObjects = GameObject.FindGameObjectsWithTag("Hand");

            // Überprüft, ob mindestens zwei Hände gefunden wurden.
            if (touchObjects != null && touchObjects.Length > 1)
            {
                foreach (GameObject touchObject in touchObjects)
                {
                    // Übergibt das Objekt, das berührt werden soll,
                    // an die TouchHand-Komponente.
                    touchObject.GetComponent<TouchHand>()
                        .SetTouchTarget(touchTarget);

                    // Übergibt die ersten beiden gefundenen Hände,
                    // damit die TouchHand-Komponente auf beide
                    // Hand-Objekte zugreifen kann.
                    touchObject.GetComponent<TouchHand>()
                        .SetHands(new List<GameObject>()
                        {
                            touchObjects[0],
                            touchObjects[1]
                        });
                }

                // Deaktiviert die Suche nach weiteren Hand-Objekten.
                isactive = false;
            }
        }
    }

    /// <summary>
    /// Setzt den Abschlussstatus der ersten beiden Hände zurück,
    /// damit die Aufgabe erneut durchgeführt werden kann.
    /// </summary>
    protected override void CompReset()
    {
        // Setzt den Aufgabenstatus der ersten Hand zurück.
        touchObjects[0].GetComponent<TouchHand>()
            .SetTaskFinished(false);

        // Setzt den Aufgabenstatus der zweiten Hand zurück.
        touchObjects[1].GetComponent<TouchHand>()
            .SetTaskFinished(false);
    }

    /// <summary>
    /// Wartet auf den Abschluss der Aufgabe durch die erste Hand.
    /// Spielt anschließend den Handschuh-Sound ab und beendet die Task.
    /// </summary>
    protected override IEnumerator TaskRunActive()
    {
        // Wartet, bis die Hand-Objekte initialisiert wurden.
        while (touchObjects == null)
        {
            yield return new WaitForFixedUpdate();
        }

        // Wartet, bis die erste Hand den Aufgabenabschluss meldet.
        while (!touchObjects[0].GetComponent<TouchHand>()
            .GetTaskCompletion())
        {
            yield return new WaitForFixedUpdate();
        }

        // Spielt den Sound "Latex Glove" ab.
        FindObjectOfType<SoundManager>()
            .ManageSound("Latex Glove", true, 1f);

        // Beendet die Aufgabe.
        EndTask();
    }
}
