using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClipPressObject : PressObject
{
    public InputActionReference toggleReference = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake() => toggleReference.action.started += Toggle;
    private void OnDestroy() => toggleReference.action.started -= Toggle;
    private void Toggle(InputAction.CallbackContext context) // Wird aufgerufen, wenn der Button für toggleReference gedrückt wird -> siehe Samples/Default Input Actions/XRI Default Input Actions
    {
        base.Press();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
