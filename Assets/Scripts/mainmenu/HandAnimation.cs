using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animation anim;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void staticHands()
    {
        anim = GameObject.Find("LeftHand").GetComponent<Animation>();
        anim.enabled = false;

    }
}
