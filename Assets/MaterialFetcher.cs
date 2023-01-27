using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFetcher : MonoBehaviour
{
    private Material Fetch(GameObject target,string tag)
    {
        Material materialToGet = null;
        foreach (Transform t in target.transform)
        {
            if (t.childCount > 0)
                Fetch(t.gameObject,tag);
            if (t.tag == tag)
            {
                materialToGet = t.GetComponent<MeshRenderer>().material;
            }
        }
        return materialToGet;
    }
    public void MaterialChange(GameObject providedObject, GameObject target, string tag)
    {
        foreach (Transform t in providedObject.transform)
        {
            if (t.childCount > 0)
                MaterialChange(t.gameObject, target, tag);
            if (t.tag == tag)
            {
                Material otherMat = GetComponent<MaterialFetcher>().Fetch(target,tag);
                t.gameObject.GetComponent<MeshRenderer>().material = otherMat;
            }
        }
    }
}
