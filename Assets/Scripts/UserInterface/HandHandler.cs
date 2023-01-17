using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;

/* Bezieht sich Auf die Darstellung der Controller Hilfe und den Warnungen während mancher Tasks */
public class HandHandler : MonoBehaviour
{
    public GameObject controls; // on default disabled
    public GameObject right_ray_interactor; // on default enabled
    public GameObject left_ray_interactor; // on default enabled
    public InputActionReference toggleReferenceControl_right = null;

    public GameObject warning; // on default disabled

    public TextMeshProUGUI[] warning_texts;
    Image warning_sign;
    Image warning_image;

    private void Awake()
    {
        warning.SetActive(true);            //written,

        warning_texts[0] = warning.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        warning_texts[1] = warning.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        warning_sign = warning.transform.GetChild(2).gameObject.GetComponent<Image>();
        warning_image = warning.GetComponent<Image>();

        // Opasity auf 0
        warning_texts[0].color = new Color(warning_texts[0].color.r, warning_texts[0].color.g, warning_texts[0].color.b, 0);
        warning_texts[1].color = new Color(warning_texts[1].color.r, warning_texts[1].color.g, warning_texts[1].color.b, 0);
        warning_sign.color = new Color(warning_sign.color.r, warning_sign.color.g, warning_sign.color.b, 0);
        warning_image.color = new Color(warning_image.color.r, warning_image.color.g, warning_image.color.b, 0);

        toggleReferenceControl_right.action.started += ShowControls;


        //-----------------------------------------------------------------------------------------------------------------------

        

    }

    private void OnDestroy()
    {
        toggleReferenceControl_right.action.started -= ShowControls;        //change from toggleReferenceControl_right.action.started -= ShowControls;
    }
    public void ShowControls(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReferenceControl_right gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        bool isActive = !controls.activeSelf;
        controls.SetActive(isActive);

        var right_hand_ray = right_ray_interactor.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>(); // Deaktiviert die Rays beim aktivieren des Control-Help-Panels
        right_hand_ray.gameObject.SetActive(!isActive);

        var left_hand_ray = left_ray_interactor.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>();
        left_hand_ray.gameObject.SetActive(!isActive);
    }

    public void Warning(int warningIndex) // Wird aufgerufen, wenn UI Beide Haende benutzen anzeigen soll
    {
        StartCoroutine(ShowWarning(warningIndex, 0.1f, 0.12f));
    }
    /**
     * warningIndex: Welche Warning dargestellt wird
     * wait: länge der Wartezeit zwischen fade
     * value: Opasity Änderung per Durchgang
     **/
    IEnumerator ShowWarning(int warningIndex, float wait, float value)
    {
        warning_texts[warningIndex].gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            while (warning_image.color.a < 1.0f && warning_texts[warningIndex].color.a < 1.0f) // fade in
            {
                warning_texts[warningIndex].color = new Color(warning_texts[warningIndex].color.r, warning_texts[warningIndex].color.g, warning_texts[warningIndex].color.b, warning_texts[warningIndex].color.a + value);
                warning_sign.color = new Color(warning_sign.color.r, warning_sign.color.g, warning_sign.color.b, warning_sign.color.a + value);
                warning_image.color = new Color(warning_image.color.r, warning_image.color.g, warning_image.color.b, warning_image.color.a + value);
                yield return new WaitForSeconds(wait);
            }
            while (warning.GetComponent<Image>().color.a > 0.0f && warning_texts[warningIndex].color.a > 0.0f) // fade out
            {
                warning_texts[warningIndex].color = new Color(warning_texts[warningIndex].color.r, warning_texts[warningIndex].color.g, warning_texts[warningIndex].color.b, warning_texts[warningIndex].color.a - value);
                warning_sign.color = new Color(warning_sign.color.r, warning_sign.color.g, warning_sign.color.b, warning_sign.color.a - value);
                warning_image.color = new Color(warning_image.color.r, warning_image.color.g, warning_image.color.b, warning_image.color.a - value);
                yield return new WaitForSeconds(wait);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
