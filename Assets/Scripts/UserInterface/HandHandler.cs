using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;

public class HandHandler : MonoBehaviour
{
    public GameObject controls; // on default disabled
    public GameObject right_ray_interactor; // on default enabled
    public GameObject left_ray_interactor; // on default enabled
    public InputActionReference toggleReferenceControl_right = null;

    public GameObject warning; // on default disabled

    TextMeshProUGUI warning_text;
    Image warning_sign;
    Image warning_image;

    public void Start() // For Warning
    {
        warning_text = warning.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        warning_sign = warning.transform.GetChild(1).gameObject.GetComponent<Image>();
        warning_image = warning.GetComponent<Image>();

        warning_text.color = new Color(warning_text.color.r, warning_text.color.g, warning_text.color.b, 0);
        warning_sign.color = new Color(warning_sign.color.r, warning_sign.color.g, warning_sign.color.b, 0);
        warning_image.color = new Color(warning_image.color.r, warning_image.color.g, warning_image.color.b, 0);
    }
    private void Awake()
    {
        toggleReferenceControl_right.action.started += ShowControls;
    }

    private void OnDestroy()
    {
        toggleReferenceControl_right.action.started -= ShowControls;
    }
    public void ShowControls(InputAction.CallbackContext context)
    {
        bool isActive = !controls.activeSelf;
        controls.SetActive(isActive);

        //Disabled die Rays beim aktivieren des Control-Help-Panels
        var right_hand_ray = right_ray_interactor.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>(); //Disabled die Rays beim aktivieren des Control-Help-Panels
        right_hand_ray.gameObject.SetActive(!isActive);

        var left_hand_ray = left_ray_interactor.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>();
        left_hand_ray.gameObject.SetActive(!isActive);
    }

    public void Warning() // Wird aufgerufen, wenn UI Beide Haende benutzen anzeigen soll
    {
        StartCoroutine(ShowWarning(0.1f, 0.12f));
    }

    IEnumerator ShowWarning(float wait, float value)
    {  
        for (int i = 0; i < 3; i++)
        {
            while (warning_image.color.a < 1.0f && warning_text.color.a < 1.0f)
            {
                warning_text.color = new Color(warning_text.color.r, warning_text.color.g, warning_text.color.b, warning_text.color.a + value);
                warning_sign.color = new Color(warning_sign.color.r, warning_sign.color.g, warning_sign.color.b, warning_sign.color.a + value);
                warning_image.color = new Color(warning_image.color.r, warning_image.color.g, warning_image.color.b, warning_image.color.a + value);
                yield return new WaitForSeconds(wait);
            }
            while (warning.GetComponent<Image>().color.a > 0.0f && warning_text.color.a > 0.0f)
            {
                warning_text.color = new Color(warning_text.color.r, warning_text.color.g, warning_text.color.b, warning_text.color.a - value);
                warning_sign.color = new Color(warning_sign.color.r, warning_sign.color.g, warning_sign.color.b, warning_sign.color.a - value);
                warning_image.color = new Color(warning_image.color.r, warning_image.color.g, warning_image.color.b, warning_image.color.a - value);
                yield return new WaitForSeconds(wait);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
