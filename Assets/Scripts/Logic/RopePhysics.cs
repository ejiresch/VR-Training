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

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Vector3 ropeNodePos = startPoint.position;

        for (int i = 0; i < ropesections; i++)
        {
            RopeNode n = new RopeNode(ropeNodePos);
            nodes.Add(n);
            ropeNodePos.y -= ropeNodeLength;
        }
    }

    void Update()
    {
        UpdateRopeSimulation();

        // Fix first node to start point
        RopeNode firstNode = nodes[0];
        firstNode.pos = startPoint.position;
        nodes[0] = firstNode;

        // Apply constraints multiple times
        for (int i = 0; i < 50; i++)
        {
            MaximumStretch();
        }

        DisplayRope();
    }

    private void UpdateRopeSimulation()
    {
        Vector3 gravityVec = new Vector3(0f, gravity, 0f);
        float t = Time.deltaTime;

        for (int i = 1; i < nodes.Count; i++)
        {
            RopeNode current = nodes[i];

            Vector3 velocity = current.pos - current.oldPos;
            velocity *= 0.99f; // damping

            current.oldPos = current.pos;
            current.pos += velocity;
            current.pos += gravityVec * t * t;

            nodes[i] = current;
        }
    }

    private void MaximumStretch()
    {
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            RopeNode top = nodes[i];
            RopeNode bot = nodes[i + 1];

            float dist = Vector3.Distance(top.pos, bot.pos);
            float error = dist - ropeNodeLength;

            Vector3 changeDir = (top.pos - bot.pos).normalized;
            Vector3 change = changeDir * error;

            if (i == 0)
            {
                bot.pos += change;
                nodes[i + 1] = bot;
            }
            else if (i + 1 == nodes.Count - 1 && endPoint != null)
            {
                bot.pos = endPoint.position;
                nodes[i + 1] = bot;
            }
            else
            {
                bot.pos += change * 0.5f;
                top.pos -= change * 0.5f;

                nodes[i + 1] = bot;
                nodes[i] = top;
            }
        }
    }

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