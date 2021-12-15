using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manometer_movement : MonoBehaviour
{
    public GameObject nadel;
    public InputActionReference toggleRef = null;

    private void Awake()
    {
        toggleRef.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleRef.action.started -= Toggle;

    }
    private void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        //gameObject.SetActive(isActive);

        if (isActive)
        {
            StartCoroutine(Druecken());
        }
        else
        {
            //StartCoroutine(Loslassen());
        }
    }

    IEnumerator Druecken()
    {
        for (int i = 0; i < 10; i++)
        {
            nadel.transform.Rotate(new Vector3(0, 3, 0));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
