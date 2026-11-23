using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityActionTask_Change_status : MonoBehaviour
{
    public ProximityObject changer;
    public ProximityObject changeable;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(changer.transform.postion == changeable.transform.position )
        {
            changeable.onCollisionEnter(changer);
        }
}
