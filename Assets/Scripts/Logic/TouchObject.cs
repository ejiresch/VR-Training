using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Ein Objekt, welche bei der Beruehrung zweier Objekte die Task beendet
public class TouchObject : InteractableObject
{
    private GameObject touchTarget;
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.Equals(touchTarget))
        {
            MaterialFetcher matF = GetComponent<MaterialFetcher>();
            if (matF != null)
            {
                matF.MaterialChange(this.gameObject, touchTarget, "touchMaterial"); 
            }
            taskfinished = true;
            touchTarget = null;
        }
    }
    // Setzt das Ziel des TouchObjects
    public void SetTouchTarget(GameObject touchTarger) => touchTarget = touchTarger;
}
