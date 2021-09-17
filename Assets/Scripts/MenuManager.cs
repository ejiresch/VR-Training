using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace b
{
    public class MenuManager : MonoBehaviour
    {
        public TextMeshPro text;
        public void PressButton()
        {
            text.SetText("change");
        }
    }
}
