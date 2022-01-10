using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompundPart : InteractableObject
{
    private bool taskFocus;
    // Start is called before the first frame update
    void Start()
    {
        taskFocus = false;
    }
    public override void OnDrop()
    {
        base.OnDrop();
        if (taskFocus)
        {
            ProcessHandler.Instance.NextTask();
        }
    }
}
