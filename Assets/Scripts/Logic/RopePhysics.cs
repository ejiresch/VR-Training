using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates a rope using Verlet integration.
/// Fixes included:
/// - Proper anchoring (pos + oldPos)
/// - No hard snapping inside constraint solver
/// - Stable constraint distribution
/// - Reduced jitter and rebound
/// </summary>
public class RopePhysics : MonoBehaviour
{
    [Header("Rope Setup")]

    /// <summary>Starting point of the rope (fixed).</summary>
    public Transform startPoint;

    /// <summary>Optional end point of the rope (fixed if assigned).</summary>
    public Transform endPoint;

    /// <summary>Number of segments (nodes) in the rope.</summary>
    public int ropesections = 15;

    /// <summary>Gravity applied to the rope.</summary>
    public float gravity = -9.81f;

    /// <summary>Length between each rope node.</summary>
    public float ropeNodeLength = 0.5f;

    /// <summary>Visual width of the rope.</summary>
    public float ropeWidth = 0.2f;

    [Header("Stability")]

    /// <summary>How stiff the rope is (0–1).</summary>
    public float stiffness = 0.5f;

    /// <summary>Number of constraint solver iterations.</summary>
    public int constraintIterations = 15;

    /// <summary>Velocity damping to reduce jitter.</summary>
    public float damping = 0.98f;

    private List<RopeNode> nodes = new List<RopeNode>();
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Vector3 ropeNodePos = startPoint.position;

        for (int i = 0; i < ropesections; i++)
        {
            nodes.Add(new RopeNode(ropeNodePos));
            ropeNodePos.y -= ropeNodeLength;
        }
    }

    void Update()
    {
        UpdateRopeSimulation();

        // ✅ Anchor BOTH ends BEFORE constraint solving
        ApplyAnchors();

        // ✅ Solve constraints multiple times for stability
        for (int i = 0; i < constraintIterations; i++)
        {
            ApplyConstraints();
            ApplyAnchors(); // re-apply anchors each iteration
        }

        DisplayRope();
    }

    /// <summary>
    /// Applies Verlet integration.
    /// Uses previous position to simulate velocity.
    /// </summary>
    private void UpdateRopeSimulation()
    {
        Vector3 gravityVec = new Vector3(0f, gravity, 0f);
        float t = Time.deltaTime;

        for (int i = 1; i < nodes.Count; i++)
        {
            RopeNode node = nodes[i];

            // Calculate velocity
            Vector3 velocity = node.pos - node.oldPos;

            // Apply damping to reduce jitter
            velocity *= damping;

            // Store current position
            node.oldPos = node.pos;

            // Apply motion
            node.pos += velocity;
            node.pos += gravityVec * t * t;

            nodes[i] = node;
        }
    }

    /// <summary>
    /// Ensures anchor nodes do not introduce fake velocity.
    /// IMPORTANT: must set BOTH pos and oldPos.
    /// </summary>
    private void ApplyAnchors()
    {
        // Start point anchor
        RopeNode first = nodes[0];
        first.pos = startPoint.position;
        first.oldPos = startPoint.position; // prevents fake velocity
        nodes[0] = first;

        // End point anchor (if assigned)
        if (endPoint != null)
        {
            int lastIndex = nodes.Count - 1;
            RopeNode last = nodes[lastIndex];
            last.pos = endPoint.position;
            last.oldPos = endPoint.position; // prevents drift/offset
            nodes[lastIndex] = last;
        }
    }

    /// <summary>
    /// Applies distance constraints between nodes.
    /// Keeps segments at constant length.
    /// FIXES:
    /// - No hard snapping inside solver
    /// - Balanced correction between nodes
    /// - Uses stiffness to prevent jitter
    /// </summary>
    private void ApplyConstraints()
    {
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            RopeNode nodeA = nodes[i];
            RopeNode nodeB = nodes[i + 1];

            float dist = Vector3.Distance(nodeA.pos, nodeB.pos);
            float error = dist - ropeNodeLength;

            Vector3 dir = (nodeA.pos - nodeB.pos).normalized;
            Vector3 change = dir * error * stiffness;

            // If first node → only move B (A is anchored)
            if (i == 0)
            {
                nodeB.pos += change;
                nodes[i + 1] = nodeB;
            }
            // If last node is anchored → only move A
            else if (i + 1 == nodes.Count - 1 && endPoint != null)
            {
                nodeA.pos -= change;
                nodes[i] = nodeA;
            }
            else
            {
                // Distribute correction evenly
                nodeA.pos -= change * 0.5f;
                nodeB.pos += change * 0.5f;

                nodes[i] = nodeA;
                nodes[i + 1] = nodeB;
            }
        }
    }

    /// <summary>
    /// Renders the rope using LineRenderer.
    /// </summary>
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

    /// <summary>
    /// Represents a rope node.
    /// Stores current and previous position.
    /// </summary>
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