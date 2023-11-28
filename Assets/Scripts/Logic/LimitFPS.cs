using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    public int fps = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = fps;
    }

}
