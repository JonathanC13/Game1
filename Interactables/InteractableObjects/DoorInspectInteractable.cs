using UnityEngine;

public class DoorInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    public event System.Action<DoorInspectInteractable> OnInteracted;

    public override void Interact()
    {
        cameraStateMachine.CameraTransition.Configure(
            inspectView,
            cameraStateMachine.inspectFOV,
            cameraStateMachine.Inspecting);

        cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);

        OnInteracted?.Invoke(this);
    }
}
