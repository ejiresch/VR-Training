using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// Describes a collision Task, where two objects have to collide to be completed
public class CollisionTask : Task
{
    public GameObject tool, otherTool;
    // Checks the success condition
    public override bool IsSuccessful(CollisionEvent ce)
    {
        string firstToolName = ce.First.name.Replace("(Clone)", "");
        string secondToolName = ce.Second.name.Replace("(Clone)", "");
        if (tool.name.Equals(firstToolName) || tool.name.Equals(secondToolName))
        {
            if (otherTool.name.Equals(firstToolName) || otherTool.name.Equals(secondToolName)) return true;
        }
        return false;
    }
}
