using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Stellt Methoden zum Ändern von Material zur verfügung.
/// </summary>
/// <remarks>Autor: Marvin Fornezzi</remarks>
public class MaterialFetcher : MonoBehaviour
{
    private Material materialToGet = null;
    /// <summary>
    /// Sucht das Material am Zielobjekt und gibt dieses zurück.
    /// <br> Tag mus angegeben werden um festzustellen wo das Material am GameObject zu finden ist.</br>
    /// </summary>
    /// <param name="target"> Das GameObject auf dem das Material gesucht wird</param>
    /// <param name="tag"> Der Tag der Komponente (des GameObject) dessen Material gesucht wird. </param>
    /// <returns></returns>
    private Material Fetch(GameObject target,string tag)
    {
        foreach (Transform t in target.transform)
        {
            if (t.childCount > 0)
                Fetch(t.gameObject,tag);
            if (t.tag == tag && materialToGet == null)
            {
                materialToGet = t.GetComponent<Renderer>().material;
            }
        }
        return materialToGet;
    }
    /// <summary>
    /// Setzt das Material der Komponente des providedObject, auf das Material des TargetObject.
    /// </summary>
    /// <param name="providedObject"> GameObject dessen Material verändert werden soll.</param>
    /// <param name="target">GameObject von dem das Material genommen werden soll.</param>
    /// <param name="tag"> Tag den die Komponente hat dessen Material gesucht und gesetzt wird. </param>
    public void MaterialChange(GameObject providedObject, GameObject target, string tag)
    {
        foreach (Transform t in providedObject.transform)
        {
            if (t.childCount > 0)
                MaterialChange(t.gameObject, target, tag);
            if (t.tag == tag)
            {
                Material otherMat = GetComponent<MaterialFetcher>().Fetch(target,tag);
                t.gameObject.GetComponent<Renderer>().material = otherMat;
            }
        }
        materialToGet=null;
    }
}
