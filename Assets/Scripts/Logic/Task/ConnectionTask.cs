
using development_a;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Diese Task ist für das Verbinden eines ConnectorObject mit einem
/// Connectible-Objekt zuständig.
/// 
/// Zu Beginn der Task werden beide Objekte gesucht und miteinander
/// verknüpft. Anschließend wartet die Task, bis die Verbindung
/// erfolgreich abgeschlossen wurde.
/// </summary>
public class ConnectionTask : Task
{
    // Das ConnectorObject, das zum Herstellen der Verbindung verwendet wird.
    public GameObject connector;

    // Das Connectible-Objekt, mit dem der Connector verbunden werden soll.
    public GameObject connectible;

    /// <summary>
    /// Initialisiert die Verbindungstask.
    /// 
    /// Zuerst wird die StartTask-Methode der Basisklasse aufgerufen.
    /// Danach werden Connector und Connectible über ihre Namen gesucht.
    /// Wenn beide Objekte gefunden wurden, wird der Connector aktiviert
    /// und dem Connectible-Objekt zugewiesen.
    /// </summary>
    public override void StartTask()
    {
        // Führt die Initialisierung der übergeordneten Task-Klasse aus.
        base.StartTask();

        // Sucht den Connector im verfügbaren Werkzeugbestand.
        connector = base.FindTool(connector.name);

        // Sucht das Objekt, mit dem der Connector verbunden werden soll.
        connectible = base.FindTool(connectible.name);

        // Prüft, ob beide Objekte erfolgreich gefunden wurden.
        if (connector != null && connectible != null)
        {
            // Aktiviert den Connector für den Verbindungsvorgang.
            connector.GetComponent<ConnectorObject>().connectorActive = true;

            // Übergibt den Connector an das Connectible-Objekt,
            // damit dieses die Verbindung verwalten kann.
            connectible.GetComponent<Connectible>()
                .SetConnector(connector.GetComponent<ConnectorObject>());
        }
    }

    /// <summary>
    /// Setzt den Abschlussstatus des Connectors zurück.
    /// Dadurch kann die Verbindungstask erneut ausgeführt werden.
    /// </summary>
    protected override void CompReset()
    {
        // Markiert die Aufgabe des Connectors wieder als nicht abgeschlossen.
        connector.GetComponent<ConnectorObject>().SetTaskFinished(false);
    }

    /// <summary>
    /// Überwacht den Verbindungsfortschritt.
    /// 
    /// Die Coroutine wartet, bis das ConnectorObject meldet,
    /// dass die Verbindung abgeschlossen wurde. Anschließend wird
    /// ein Sound abgespielt und die Task beendet.
    /// </summary>
    protected override IEnumerator TaskRunActive()
    {
        // Wartet, bis der Connector den Abschluss der Verbindung meldet.
        while (!connector.GetComponent<ConnectorObject>()
                         .GetTaskCompletion())
        {
            // Wartet bis zum nächsten FixedUpdate-Zyklus.
            yield return new WaitForFixedUpdate();
        }

        // Spielt den Sound "Plop" ab, sobald die Verbindung abgeschlossen ist.
        FindObjectOfType<SoundManager>()
            .ManageSound("Plop", true, 1f);

        // Beendet die aktuelle Task.
        EndTask();

        // Prüft, ob die Jump-Taste gedrückt wurde.
        if (Input.GetButtonDown("Jump"))
        {
            // Setzt den Connector auf abgeschlossen.
            connector.GetComponent<ConnectorObject>()
                .SetTaskFinished(true);
        }
    }
}
