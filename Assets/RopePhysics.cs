using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour
{

    public Transform startPoint;
    public Transform endPoint;
    public int ropesections = 15;
    public float gravity = -9.81f;
    public float ropeNodeLength = 0.005f;
    public float ropeWidth = 0.002f;

    private List<RopeNode> nodes = new List<RopeNode>();
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Vector3 ropeNodePos = startPoint.position;

        for (int i = 0; i < ropesections; i++)
        {
            GameObject node = Instantiate(new GameObject(),ropeNodePos, Quaternion.identity, transform);
            node.AddComponent<SphereCollider>().radius = ropeWidth * 50;
            node.AddComponent<Rigidbody>().useGravity = false;
            RopeNode n = new RopeNode(ropeNodePos, node);
            nodes.Add(n);
            ropeNodePos.y -= ropeNodeLength;
        }
        //endPoint.transform.parent.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayRope();
    }

    private void DisplayRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;

        Vector3[] positions = new Vector3[nodes.Count];

        for (int i = 0; i < nodes.Count; i++)
        {
            positions[i] = nodes[i].go.transform.position;
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    private void FixedUpdate()
    {
        UpdateRopeSimulation();
        UpdateColliders();

        RopeNode firstNode = nodes[0];
        firstNode.pos = startPoint.position;
        nodes[0] = firstNode;

        RopeNode lastNode = nodes[nodes.Count - 1];
        lastNode.pos = endPoint.position;
        /*endPoint.position = lastNode.pos; */
        nodes[nodes.Count - 1] = lastNode;
    }

    private void UpdateRopeSimulation()
    {
        Vector3 gravity = new Vector3(0f, this.gravity, 0f);

        float t = Time.fixedDeltaTime;

        for (int i = 1; i < nodes.Count; i++)
        {
            RopeNode current = nodes[i];
            Vector3 vel = current.pos - current.oldPos;
            current.oldPos = current.pos;
            if (!current.collided)
            {
                current.pos += vel;
                current.pos += gravity * t;
            }
            else
            {
                Vector3 v = current.pos - current.go.transform.position;
                current.pos += v;
            }
            nodes[i] = current;
        }

        for (int i = 0; i < 20; i++)
        {
            MaximumStretch();
        }
    }

    private void MaximumStretch()
    {
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            RopeNode top = nodes[i];
            RopeNode bot = nodes[i + 1];

            float dist = (top.pos - bot.pos).magnitude;
            float distError = Mathf.Abs(dist - ropeNodeLength);

            Vector3 changeDir = Vector3.zero;

            if (dist > ropeNodeLength)
            {
                changeDir = (top.pos - bot.pos).normalized;
            }
            else if (dist < ropeNodeLength)
            {
                changeDir = (bot.pos - top.pos).normalized;
            }

            Vector3 change = changeDir * distError;

            if (i != 0 && i + 1 != nodes.Count - 1)
            {
                bot.pos += change * ropeNodeLength;
                nodes[i + 1] = bot;
                top.pos -= change * ropeNodeLength;
                nodes[i] = top;
            }
            else if (i == 0)
            {
                bot.pos += change;
                nodes[i + 1] = bot;
            }
            else
            {
                top.pos -= change;
                nodes[i] = top;
            }
        }
    }

    public void UpdateColliders()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].go.transform.position = nodes[i].pos;
        }
    }

    public struct RopeNode
    {
        public Vector3 pos;
        public Vector3 oldPos;
        public GameObject go;
        public bool collided;

        public RopeNode(Vector3 pos, GameObject go)
        {
            this.pos = pos;
            this.oldPos = pos;
            this.go = go;
            this.collided = false;
        }

    }
}
