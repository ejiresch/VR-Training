using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Task, which is responsible for closing/opening the bands around the "kanüle"
public class BandControlTask : Task
{
    [SerializeField] private GameObject bandLinks, bandRechts;
    [SerializeField] private InputActionReference toggleReference = null;
    [SerializeField] private float range = 0.11f;
    [SerializeField] private bool inReverse = false;
    private Animator[] animators;
    private Vector3 centerVector;
    private GameObject[] hands;

    private void Awake() => toggleReference.action.started += CheckDistance;

    private void OnDestroy() => toggleReference.action.started -= CheckDistance;
    // Anfang der Task
    public override void StartTask()
    {
        base.StartTask();
        bandLinks = base.FindTool(bandLinks.name);
        bandRechts = base.FindTool(bandRechts.name);
        centerVector = (bandLinks.transform.position + bandRechts.transform.position) / 2;
        // Finden der Referenzen
        hands = GameObject.FindGameObjectsWithTag("Hand");
        animators = new Animator[2];
        animators[0] = bandLinks.GetComponentInChildren<Animator>();
        animators[1] = bandRechts.GetComponentInChildren<Animator>();
    }
    // Checkt die Distanz vom Mittelpunkt zu dem definierten Radius und schließt die Task ab, wenn diese im Toleranzbereich liegt.
    public void CheckDistance(InputAction.CallbackContext context)
    {
        Vector3 handsCenter = new Vector3();
        foreach(GameObject hand in hands)
        {
            handsCenter += hand.transform.position;
        }
        handsCenter /= hands.Length;
        if (Vector3.Distance(handsCenter, centerVector) <= range)
        {
            if (!inReverse)
            {
                bandLinks.transform.Rotate(new Vector3(220, 0, 0));
                bandRechts.transform.Rotate(new Vector3(130, 180, 0));
            }
            foreach (Animator anime in animators) anime.SetTrigger("biegen");
            ProcessHandler.Instance.NextTask();
        }
    }

}
