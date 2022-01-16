using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    OVRHapticsClip clip = new OVRHapticsClip();

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<2; i++)
        {
            clip.WriteSample(i % 50 == 0 ? (byte)255 : (byte)0);
        }
        OVRHaptics.LeftChannel.Preempt(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
