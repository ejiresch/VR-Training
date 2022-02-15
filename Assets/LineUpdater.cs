using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpdater : MonoBehaviour
{
    public GameObject connector, connectee;
    public LineRenderer lineRenderer;

    public void Start()
    {
        this.transform.parent = null;
        this.transform.position = new Vector3(0, 0, 0);
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        connectee.transform.parent.GetComponent<Rigidbody>().isKinematic = false;

    }
    private void Update()
    {
        lineRenderer.SetPosition(0, connector.transform.position);
        lineRenderer.SetPosition(1, connectee.transform.position);
    }
}
