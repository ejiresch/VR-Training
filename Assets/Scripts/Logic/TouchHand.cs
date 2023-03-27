using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Ein Objekt, welche bei der Beruehrung zweier Objekte die Task beendet
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
            if (!hasfinished)
            {
                ProcessHandler.Instance.NextTask();
                hands[0].GetComponent<TouchHand>().hasfinished = true;
                hands[1].GetComponent<TouchHand>().hasfinished = true;
            }
            touchTarget = null;
        }
    }
    // Setzt das Ziel des TouchObjects
    public void SetTouchTarget(GameObject touchTarger) => touchTarget = touchTarger;
    public void SetHands(List<GameObject> hands) => this.hands = hands;
}

