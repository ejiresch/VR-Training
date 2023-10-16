using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBack : MonoBehaviour
{
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RespawnCollider")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.identity;
           
        }
    }
}
