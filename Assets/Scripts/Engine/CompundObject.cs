using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Logische Zusammenführung mehrerer Objekte zu einem Objekt, 
 * mit der Entnahme einzelner Teile (Objekte) laut der Reihenfolge aus der Queue.
 * @author Alan Matykiewicz
 */
public class CompundObject : MonoBehaviour
{
    [SerializeField] private GameObject[] parts;
    [SerializeField] private GameObject grabbablePart;
    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject go in parts) {
            go.GetComponent<InteractableObject>().SetGrabbable(false);
            objectQueue.Enqueue(go);
        }
    }

    /**
     * Gibt das nächste Objekt in der Queue aus
     * als Gameobject
     */
    public GameObject GetPart()
    {
        if(objectQueue.Count>0) return objectQueue.Dequeue();
        return null;
    }
    public GameObject GetGrabbable()
    {
        return grabbablePart;
    }
}
    