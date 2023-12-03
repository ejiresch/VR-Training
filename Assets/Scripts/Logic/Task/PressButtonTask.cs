using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressButtonTask : Task
{
    public Canvas tutorialCanvas;
    public string text;
    public HUDHandler hudHandler;

    private bool buttonClicked = false;

    /*
     * 
     * nur für jetzt übergangsweise.
     * da es Probleme gibt Transforms der Objekte die im Tutorial beschrieben werden sollen direkt zu übergeben 
     * 
     */
    private GameObject transform1;
    private GameObject transform2;
    private GameObject transform3;
    private GameObject transform4;
    private GameObject ausgewaehltTransform;
    public int transformChooser;

    public override void StartTask()
    {

        /*
         * 
         * Code für jetzt
         * Durch den transformChooser kann man zwischen den 4 hardcodeten Transforms wählen
         * 
         */
        transform1 = new GameObject("Transform1");
        transform2 = new GameObject("Transform2");
        transform3 = new GameObject("Transform3");
        transform4 = new GameObject("Transform4");
        ausgewaehltTransform = new GameObject("ausgewählt");


        transform1.transform.position = new Vector3(2.923f, -5.133f, 2.965f);

        transform2.transform.position = new Vector3(2.993f, -5.056f, -0.137f);
        transform2.transform.rotation = Quaternion.Euler(0, 180, 0);

        transform3.transform.position = new Vector3(1.66f, -5.118f, 4.409f);

        transform4.transform.position = new Vector3(0f, -4.505f, 0.964f);
        transform4.transform.rotation = Quaternion.Euler(0, -90, 0);


        switch (transformChooser)
        {
            case 1: ausgewaehltTransform = transform1; break;
            case 2: ausgewaehltTransform = transform2; break;
            case 3: ausgewaehltTransform = transform3; break;
            case 4: ausgewaehltTransform = transform4; break;
        }

        tutorialCanvas.GetComponent<RectTransform>().position = ausgewaehltTransform.transform.position;
        tutorialCanvas.GetComponent<RectTransform>().rotation = ausgewaehltTransform.transform.rotation;
        /*
         * Ende 
         */

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
            // Füge eine Funktion hinzu, die beim Klicken des Buttons aufgerufen wird
            button.onClick.AddListener(OnClick);
        }

        base.StartTask();
    }

    void OnClick()
    {
        buttonClicked = true;
        
    }


    protected override void EndTask()
    {
        hudHandler.setTutorialMode(false);
        tutorialCanvas.enabled = false;
        base.EndTask();
    }

    protected override void CompReset()
    {
        //throw new System.NotImplementedException();
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!buttonClicked)
        {
            yield return new WaitForFixedUpdate();
        }
        EndTask();
    }

}
