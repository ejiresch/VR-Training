using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour
{

    public Transform startPoint;
    public Transform endPoint;
    public int ropesections = 15;
    public float gravity = -9.81f;
    public float ropeNodeLength = 0.5f;
    public float ropeWidth = 0.2f;

    private List<RopeNode> nodes = new List<RopeNode>();
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Vector3 ropeNodePos = startPoint.position;

        for (int i = 0; i < ropesections; i++)
        {
            RopeNode n = new RopeNode(ropeNodePos);
            ropeNodePos.y -= ropeNodeLength;
            nodes.Add(n);
        }
        endPoint.transform.parent.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateRopeSimulation();

        RopeNode firstNode = nodes[0];
        firstNode.pos = startPoint.position;
        nodes[0] = firstNode;

        for (int i = 0; i < 60; i++)
        {
            MaximumStretch();
        }

        for (int i = 0; i < 20; i++)
        {
            AdjustCollision();
        }

        DisplayRope();

        RopeNode lastNode = nodes[nodes.Count - 1];
        lastNode.pos = endPoint.position;
        /*endPoint.position = lastNode.pos; */
        nodes[nodes.Count - 1] = lastNode;
    }

    // Setzt die Punkte der LineRenderer Component und updatet die Positionen der Punkte
    private void DisplayRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;

        Vector3[] positions = new Vector3[nodes.Count];

        for (int i = 0; i < nodes.Count; i++)
        {
            positions[i] = nodes[i].pos;
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    // Methode, welche die Punkte anhand der Position und der vorherigen Position berechnet
    private void UpdateRopeSimulation()
    {
        Vector3 gravity = new Vector3(0f, this.gravity, 0f);
        float t = Time.deltaTime;

        for (int i = 1; i < nodes.Count; i++)
        {
            RopeNode current = nodes[i];
            Vector3 vel = current.pos - current.oldPos;
            current.oldPos = current.pos;
            Vector3 newPos = current.pos + vel;
            newPos += gravity * t;
            current.pos = newPos;
            nodes[i] = current;
        }
    }

    private void AdjustCollision()
    {
        for(int i = 2; i < nodes.Count; i++)
        {

            if (i == nodes.Count - 2)
                break;

            RopeNode current = nodes[i];

            Collider[] colliders = Physics.OverlapSphere(current.pos, ropeWidth / 2f);

            foreach (Collider c in colliders)
            {
                Vector3 cCenter = c.transform.position;
                Vector3 cDirection = cCenter - current.pos;
                current.pos -= cDirection.normalized * Time.deltaTime;
                nodes[i] = current;
                break;
            }
        }
    }

    // setzt die Länge der Positionen auf die angegeben Ropelength
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

    // Stellt die Nodes des Ropes dar und speichert die momentane sowie die vorherige Position
    public struct RopeNode
    {
        public Vector3 pos;
        public Vector3 oldPos;

        public RopeNode(Vector3 pos)
        {
            this.pos = pos;
            this.oldPos = pos;
        }

    }
}
