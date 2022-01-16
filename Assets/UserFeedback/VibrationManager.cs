using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    OVRHapticsClip clip = new OVRHapticsClip();

    // Start is called before the first frame update
    void Start()
    {
        //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

        for(int i=0; i<2; i++)
        {
            clip.WriteSample(i % 50 == 0 ? (byte)255 : (byte)0);
        }
        OVRHaptics.LeftChannel.Preempt(clip);
        Debug.Log("sollte vibriert haben.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
