using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Transitions/Steps/Move Camera")]
public class MoveCameraStep : TransitionStepAsset
{
    public override IEnumerator Execute(
        TransitionContext context,
        TransitionRequest request)
    {
        context.CameraRig.MoveTo(request.CameraDestination, request.FOVDestination);

        while (context.CameraRig.IsMoving)
            yield return null;
    }
}