using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Transitions/Steps/Fade Out")]
public class FadeOutStep : TransitionStepAsset
{
    public float duration = 0.5f;

    public override IEnumerator Execute(
        TransitionContext context,
        TransitionRequest request)
    {
        yield return context.ScreenFader
            .FadeOut(duration);
    }
}