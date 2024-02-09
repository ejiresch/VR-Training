using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrabTaskWithCanvas : GrabTask
{

    public string text;


    public GameObject cam;

    public override void StartTask()
    {
        StartCoroutine(TaskStarter());       
    }

    private IEnumerator TaskStarter()
    {
        base.StartTask();
        cam = FindObjectOfType<Camera>().gameObject;

        tutorialCanvas.GetComponent<RectTransform>().position = grabObject.transform.position + Vector3.up * 0.2f;
        tutorialCanvas.GetComponent<RectTransform>().LookAt(cam.transform);

        Vector3 drehung = new Vector3(0, 180 + tutorialCanvas.GetComponent<RectTransform>().rotation.eulerAngles.y, tutorialCanvas.GetComponent<RectTransform>().rotation.eulerAngles.z);

        tutorialCanvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(drehung);

        //Wartezeit ist anders als bei anderen Tutorial Tasks da es mit dem Timing so besser passt. Sollte man probably nicht hardcoden da es nur für einen spezifischen Fall ist, aber ich glaube das ist eine Aufgabe für die Kollegen im nächsten Jahr
        yield return new WaitForSeconds(1.5f);
        tutorialCanvas.enabled = true;
        hudHandler.setTutorialMode(true);
        hudHandler.setLookDirection(tutorialCanvas.transform);

        // Finde den Text im Canvas
        TextMeshProUGUI[] text = tutorialCanvas.GetComponentsInChildren<TextMeshProUGUI>();
        // [1] da sonnst nur der TextMeshPro des Buttons gefunden wird
        text[1].text = this.text;

        // Finde den Button im Canvas
        Button button = tutorialCanvas.GetComponentInChildren<Button>();

        if (button != null)
        {
            button.enabled = false;
            button.GetComponent<Image>().enabled = false;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
    }


    public void setText(string text)
    {
        this.text = text;
    }

    /*
    protected override void EndTask()
    {
        hudHandler.setTutorialMode(false);
        tutorialCanvas.enabled = false;
        base.EndTask();
        Button button = tutorialCanvas.GetComponentInChildren<Button>();

        if (button != null)
        {
            button.enabled = true;
            button.GetComponent<Image>().enabled = true;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
        Debug.Log("Task ended (Grab Task)");

    }
    */
}
