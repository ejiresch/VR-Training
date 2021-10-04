using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            CollisionEvent ce = new CollisionEvent(this.gameObject, collision.gameObject);
            ce.ReportCollision();
        }
    }
}
