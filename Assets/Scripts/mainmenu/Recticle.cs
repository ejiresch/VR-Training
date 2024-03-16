using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recticle : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera CameraFacing;
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraFacing.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        transform.position = CameraFacing.transform.position + CameraFacing.transform.rotation * Vector3.forward* 2.0f;   
    }
}
