using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressButtonTask : Task
{
    public string text;


    private bool buttonClicked = false;

    public Vector3 textposition;
    public float rotation;

    public override void StartTask()
    {
        StartCoroutine(TaskStarter());
    }


    //this is needed in order for the Tutorial Canvas to receive the translated text
    private IEnumerator TaskStarter()
    {
        base.canvasSetup();
        base.clearCanvas();
        tutorialCanvas.GetComponent<RectTransform>().position = textposition;
        tutorialCanvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, rotation, 0);

        yield return new WaitForSeconds(0.2f);
        tutorialCanvas.enabled = true;
        tutorialBackgroundCanvas.enabled = true;
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

        base.StartTask(false);
    }


    void OnClick()
    {
        buttonClicked = true;

    }


    /*
    protected override void EndTask()
    {
        hudHandler.setTutorialMode(false);
        tutorialCanvas.enabled = false;
        base.EndTask();
    }
    */

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

    public void setText(string text)
    {
        this.text = text;
    }



}
