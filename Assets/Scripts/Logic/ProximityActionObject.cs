using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Ein Objekt, welche bei der Beruehrung zweier Objekte die Task beendet
public class ProximityActionObject : InteractableObject
{
    public InputActionReference toggleReference = null;
    public float distance = 0.1f;
    public bool hasToBeUsed = true;
    private GameObject touchTarget;
    private bool inRange = false;
    private Animator anim;
    private bool activated = false;
    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    // Setzt das Ziel des TouchObjects
    void FixedUpdate()
    {
        if (touchTarget != null)
        {
            inRange = Vector3.Distance(touchTarget.transform.position, this.transform.position) < distance;
        }
        if (inRange)
        {
            if (!hasToBeUsed)
            {
                activated = true;
                StartCoroutine(EventAnimation());
                GetComponent<MaterialFetcher>().MaterialChange(gameObject, touchTarget, "liquid");
                taskfinished = true;
            }
        }
    }
    public void SetTouchTarget(GameObject touchTarger) => touchTarget = touchTarger;
    private void Toggle(InputAction.CallbackContext context)
    {
        
        if (inRange && !activated)
        {
            activated = true;
            StartCoroutine(EventAnimation());
            GetComponent<MaterialFetcher>().MaterialChange(gameObject,touchTarget,"liquid");
            taskfinished = true;
        }
    }
    IEnumerator EventAnimation()
    {
        if(anim != null)
        {
            anim.SetTrigger("reinpumpen");
        }
        yield return new WaitForSeconds(1.3f);
    }
    public void SetTarget(GameObject touchTarger) 
    { 
        touchTarget = touchTarger;
        inRange = false;
        activated = false;
    }
    public bool GetInRange()
    {
        return inRange;
    }
    private void Awake() { 
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy() => toggleReference.action.started -= Toggle;
}
