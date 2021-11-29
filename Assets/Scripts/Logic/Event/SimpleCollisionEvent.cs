using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCollisionEvent : CollisionEvent
{
    public GameObject FirstObject { get; set; }
    public GameObject SecondObject { get; set; }

    public SimpleCollisionEvent(GameObject firstObject, GameObject secondObject)
    {
        FirstObject = firstObject;
        SecondObject = secondObject;
    }
    public override GameObject[] GetEventData()
    {
        GameObject[] daten = { FirstObject, SecondObject };
        return daten;
    }
}
