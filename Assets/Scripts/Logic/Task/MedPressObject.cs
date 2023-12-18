using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


/* Ist für die Med Crush Animation zuständig */
public class MedPressObject : PressObject, ResetInterface
{
    public InputActionReference toggleReference = null;
    public float maxDistance = 0.05f;   // Maximal Distanz zwischen Stössel und Medikament
    public Mesh crushedMesh;

    [HideInInspector]public bool crushed = false; 
    private Animator anim;
    private GameObject stoessel;
    private float dist;
    private MeshFilter meshFilter;

    private void Start()
    {
        stoessel = GameObject.FindWithTag("stoessel");  // Speichert den in der Szene vorhandenen Stössel als GO ab
        anim = this.gameObject.GetComponent<Animator>();
        meshFilter = GetComponent<MeshFilter>();
    }


    private void Awake() => toggleReference.action.started += Toggle;

    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        if (GetIsGrabbed())
        {
            if(stoessel != null)
            {
                dist = Vector3.Distance(transform.position, stoessel.transform.position);   //  Distanz zwischen Stössel und Medikament
                if(dist <= maxDistance)
                {
                    if (!pressable) return;
                    if (!crushed) StartCoroutine(Crush());
                }
            }
        }
    }
    public override void Press()
    {
        if (!pressable) return;
        
        base.Press();   //next task
        
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
        Press();   
    }
    public void ResetComp()
    {
        
    }
}
