using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI fpsText;
    private float fps;

    private void Start()
    {
        InvokeRepeating("FPSUpdate", 1, 1);
    }

    void FPSUpdate()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = fps.ToString();
    }
}
