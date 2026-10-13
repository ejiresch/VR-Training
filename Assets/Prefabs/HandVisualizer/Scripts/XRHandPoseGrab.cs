using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandPoseGrab : MonoBehaviour
{
    [Header("Hand")]
    public bool useLeftHand = true;
    public Transform trackingOrigin;

    [Header("Grab")]
    public XRDirectInteractor handInteractor;
    public XRInteractionManager interactionManager;
    public float grabRadius = 0.15f;

    XRHandSubsystem handSubsystem;
    XRGrabInteractable grabbedObject;

    readonly List<XRHandSubsystem> subsystems =
        new List<XRHandSubsystem>();

    readonly Collider[] nearby = new Collider[32];

    void OnEnable()
    {
        FindHandSubsystem();
    }

    void FindHandSubsystem()
    {
        subsystems.Clear();
        SubsystemManager.GetSubsystems(subsystems);

        foreach (var subsystem in subsystems)
        {
            if (subsystem.running)
            {
                handSubsystem = subsystem;
                break;
            }
        }
    }

    void Update()
    {
        if (handSubsystem == null || !handSubsystem.running)
        {
            FindHandSubsystem();
            return;
        }

        XRHand hand = useLeftHand
            ? handSubsystem.leftHand
            : handSubsystem.rightHand;

        if (!hand.isTracked || !IsFist(hand))
        {
            ReleaseObject();
            return;
        }

        if (grabbedObject == null)
            TryGrab(hand);
    }

    bool IsFist(XRHand hand)
    {
        if (!hand.GetJoint(XRHandJointID.Palm)
            .TryGetPose(out Pose palmPose))
            return false;

        Vector3 palm = ToWorld(palmPose.position);

        XRHandJointID[] tips =
        {
            XRHandJointID.IndexTip,
            XRHandJointID.MiddleTip,
            XRHandJointID.RingTip,
            XRHandJointID.LittleTip
        };

        foreach (var tip in tips)
        {
            if (!hand.GetJoint(tip).TryGetPose(out Pose pose))
                return false;

            if (Vector3.Distance(palm, ToWorld(pose.position))
                > 0.12f)
                return false;
        }

        return true;
    }

    Vector3 ToWorld(Vector3 position)
    {
        return trackingOrigin != null
            ? trackingOrigin.TransformPoint(position)
            : position;
    }

    void TryGrab(XRHand hand)
    {
        if (handInteractor == null || interactionManager == null)
            return;

        if (!hand.GetJoint(XRHandJointID.Palm)
            .TryGetPose(out Pose palmPose))
            return;

        Vector3 palm = ToWorld(palmPose.position);

        int count = Physics.OverlapSphereNonAlloc(
            palm, grabRadius, nearby);

        XRGrabInteractable closest = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            XRGrabInteractable candidate =
                nearby[i].GetComponentInParent<XRGrabInteractable>();

            if (candidate == null || !candidate.isActiveAndEnabled)
                continue;

            float distance = Vector3.Distance(
                palm, candidate.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = candidate;
            }
        }

        if (closest != null)
        {
            interactionManager.SelectEnter(
                (IXRSelectInteractor)handInteractor,
                (IXRSelectInteractable)closest);

            grabbedObject = closest;
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject == null)
            return;

        if (interactionManager != null && handInteractor != null)
        {
            interactionManager.SelectExit(
                (IXRSelectInteractor)handInteractor,
                (IXRSelectInteractable)grabbedObject);
        }

        grabbedObject = null;
    }
}
