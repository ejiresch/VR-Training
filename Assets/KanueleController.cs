using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanueleController : MonoBehaviour
{

    private Animator a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Submit"))
        {
            a.SetTrigger("auslassen");
        }
        if (Input.GetButtonDown("Jump"))
        {
            a.SetTrigger("aufblasen");
        }

    }
}
