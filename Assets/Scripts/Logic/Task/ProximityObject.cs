using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProximityObject : MonoBehaviour
{
    public Material Matchange;
    public bool ischanger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnColl

    void change(Material m)
    {
        this.Material = m;
    }
}
