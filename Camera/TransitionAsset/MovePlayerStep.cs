using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Transitions/Steps/Move Player")]
public class MovePlayerStep : TransitionStepAsset
{
    public override IEnumerator Execute(
        TransitionContext context,
        TransitionRequest request)
    {
        if (request.PlayerDestination != null)
        {
            context.PlayerMovement.Teleport(
                request.PlayerDestination);

            context.MouseLook.SyncFromCamera(
                context.CameraRig.transform);
        }

        yield break;
    }
}