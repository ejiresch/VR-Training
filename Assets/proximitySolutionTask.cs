
using System.Collections;
using UnityEngine;

/// <summary>
/// This task checks whether the touchObject reaches the touchTarget.
/// When the proximity condition is fulfilled, the target plays an animation.
/// </summary>
public class ProximitySolutionTask : Task
{
    public GameObject touchObject;
    public GameObject touchTarget;

    private Animator targetAnimator;
    private bool animationPlayed = false;

    public override void StartTask()
    {
        base.StartTask();

        touchObject = base.FindTool(touchObject.name);
        touchTarget = base.FindTool(touchTarget.name);

        // Get the Animator from the target
        targetAnimator = touchTarget.GetComponent<Animator>();

        // Configure proximity detection
        touchObject.GetComponent<ProximityActionObject>()
                   .SetTarget(touchTarget);
    }

    protected override void CompReset()
    {
        animationPlayed = false;

        touchObject.GetComponent<ProximityActionObject>()
                   .SetTaskFinished(false);

        // Reset animation if necessary
        targetAnimator.ResetTrigger("PlayAnimation");
    }

    protected override IEnumerator TaskRunActive()
    {
        while (!animationPlayed)
        {
            yield return null;
        }
    }

    // Call this method when the proximity condition is fulfilled
    public void PlayTargetAnimation()
    {
        if (animationPlayed)
            return;

        animationPlayed = true;

        targetAnimator.SetTrigger("PlayAnimation");
    }
}
