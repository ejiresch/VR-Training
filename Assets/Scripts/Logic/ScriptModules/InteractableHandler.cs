using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sollte das Problem lösen, das "isGrabbed" nicht in jeder Komponente extra gesetzt werden muss.
/// Diese Komponente wird automatisch hinzugefügt und muss beim XR Grab Interactable angegeben werden. 
/// </summary>
/// <remarks>Autor: Marvin Fornezzi</remarks>
public class InteractableHandler : MonoBehaviour
{
    InteractableObject[] interactableObjects;
    // Start is called before the first frame update
    void Start()
    {
        Component[] comps = GetComponents(typeof(InteractableObject));
        interactableObjects = new InteractableObject[comps.Length];
        for (int i=0; comps.Length>i;i++)
        {
            interactableObjects[i] = (InteractableObject)comps[i];
        }
        

    }
    public virtual void SetIsGrabbed(bool isg)
    {
        foreach(InteractableObject interactable in interactableObjects)
        {
            interactable.SetIsGrabbed(isg);
            if (!isg)
            {
                interactable.OnDrop();
            }
            
        }
    }
}
