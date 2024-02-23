using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Ein Objekt das an ein anderes angenähert wird und durch Verwendung des Objektes oder automatisch den Task erfüllen.  
/// </summary>
public class MedProximityActionObject : InteractableObject
{
    public InputActionReference toggleReference = null;
    public float distance = 0.1f;
    public bool hasToBeUsed = true;
    public Mesh crushedMesh;

    [HideInInspector] public bool crushed = false;
    private GameObject touchTarget;
    private bool inRange = false;
    private Animator anim;
    private bool activated = false;
    private MeshFilter meshFilter;
    private void Start()
    {
        anim = GetComponent<Animator>();
        meshFilter = GetComponent<MeshFilter>();
    }
    // Setzt das Ziel des TouchObjects
    void FixedUpdate()
    {
        if (touchTarget != null)
        {
            inRange = Vector3.Distance(touchTarget.transform.position, transform.position) < distance;
        }
        if (inRange)
        {
            if (!hasToBeUsed)
            {
                activated = true;
                StartCoroutine(Crush());
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
            StartCoroutine(Crush());
            taskfinished = true;
        }
    }
    IEnumerator Crush() // Start der "Crush" Animation
    {

        yield return new WaitForSeconds(0.5f);
        if (meshFilter != null && crushedMesh != null)
        {
            // Setze das Mesh auf das neue Mesh
            meshFilter.mesh = crushedMesh;
        }
        crushed = true;
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
    private void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy() => toggleReference.action.started -= Toggle;
}
