using UnityEngine;

public class TableInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    public override void Interact()
    {
        cameraStateMachine.CameraTransition.Configure(
            inspectView,
            cameraStateMachine.Inspecting);

        cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);
    }
}