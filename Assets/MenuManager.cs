using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public void PressButton()
    {
        TextMeshPro text = GetComponentInChildren<TextMeshPro>();
        text.SetText("Button clicked");
    }
}
