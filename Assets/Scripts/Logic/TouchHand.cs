using development_a;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Für Verwendung in einem Task, in dem ein Objekt mit einer oder beiden Händen berührt weren müssen. 
/// </summary>
[RequireComponent(typeof(MaterialFetcher))]
public class TouchHand : InteractableObject
{
    private GameObject touchTarget;
    private List<GameObject> hands;
    public bool hasfinished = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(touchTarget))
        {
            MaterialFetcher matF = GetComponent<MaterialFetcher>();
            if (matF != null)
            {
                foreach(GameObject h in hands)
                {
                    matF.MaterialChange(h, touchTarget, "touchMaterial");
                }
                
            }
            if (!taskfinished)
            {
                hands[0].GetComponent<TouchHand>().taskfinished = true;
                hands[1].GetComponent<TouchHand>().taskfinished = true;
            }
            touchTarget = null;
        }
    }
    // Setzt das Ziel des TouchObjects
    public void SetTouchTarget(GameObject touchTarger) => touchTarget = touchTarger;
    public void SetHands(List<GameObject> hands) => this.hands = hands;
}

