using System.Collections;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[CreateAssetMenu(menuName = "Transitions/Steps/Snap Camera")]
public class SnapCameraStep : TransitionStepAsset
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