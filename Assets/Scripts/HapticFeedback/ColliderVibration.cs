using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColliderVibration : MonoBehaviour
{
    public GameObject rechterController;
    public GameObject linkerController;
    XRController rechts;
    XRController links;

    // Start is called before the first frame update
    void Start()
    {
        rechts = rechterController.GetComponent<XRController>();
        links = linkerController.GetComponent<XRController>();
    }

    private void OnTriggerStay(Collider other)
    {
       


            float distanceRight = Vector3.Distance(other.transform.position, rechterController.transform.position);
            float disanceLeft = Vector3.Distance(other.transform.position, linkerController.transform.position);
            if (distanceRight < disanceLeft)
            {
                //rechts.SendHapticImpulse(0.4f, 0.1f);
            }
            else
            {
                //links.SendHapticImpulse(0.4f, 0.1f);
            }
        
       
    }
}
