using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Objects that have this script, send reports upon colliding with other interactibles
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isGrabbed = false;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            CollisionEvent ce = new CollisionEvent(this.gameObject, collision.gameObject);
            ce.ReportCollision();
        }
    }

    public bool GetIsGrabbed()
    {
        return isGrabbed;
    }

    public void SetIsGrabbed(bool isg)
    {
        this.isGrabbed = isg;
    }
}
