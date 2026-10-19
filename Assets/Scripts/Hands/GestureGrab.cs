using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GestureGrab : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor directInteractor;

    private XRGrabInteractable grabbedObject;

    public void OnGesturePerformed()
    {
        Debug.Log("Gesture performed");

        if (directInteractor == null)
        {
            Debug.Log("Direct Interactor is null");
            return;
        }
        

        if(grabbedObject != null)
        {
            Debug.Log(grabbedObject.name);
            return;
        }
            

        foreach (var target in directInteractor.interactablesHovered)
        {
            Debug.Log(target.ToString());
            if (target is XRGrabInteractable grab)
            {
                directInteractor.interactionManager.SelectEnter(
                    (UnityEngine.XR.Interaction.Toolkit.IXRSelectInteractor)directInteractor,
                    (UnityEngine.XR.Interaction.Toolkit.IXRSelectInteractable)grab
                );

                grabbedObject = grab;
                Debug.Log("Grab = " + grabbedObject.name);
                return;
            }
        }
    }

    public void OnGestureEnded()
    {
        Debug.Log("Gesture stopped");

        if (directInteractor == null || grabbedObject == null)
            return;

        directInteractor.interactionManager.SelectExit(
            (UnityEngine.XR.Interaction.Toolkit.IXRSelectInteractor)directInteractor,
            (UnityEngine.XR.Interaction.Toolkit.IXRSelectInteractable)grabbedObject
        );

        grabbedObject = null;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
