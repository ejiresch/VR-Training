using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrabTaskWithCanvas : GrabTask
{
    public Canvas tutorialCanvas;
    public string text;
    public HUDHandler hudHandler;

    public GameObject cam;

    public override void StartTask()
    {
        base.StartTask();
        cam = FindObjectOfType<Camera>().gameObject;

        tutorialCanvas.GetComponent<RectTransform>().position = grabObject.transform.position + Vector3.up*0.2f;
        tutorialCanvas.GetComponent<RectTransform>().LookAt(cam.transform);

        Vector3 drehung = new Vector3(0, 180 +tutorialCanvas.GetComponent<RectTransform>().rotation.eulerAngles.y, tutorialCanvas.GetComponent<RectTransform>().rotation.eulerAngles.z);

        tutorialCanvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(drehung);


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

    }
}
