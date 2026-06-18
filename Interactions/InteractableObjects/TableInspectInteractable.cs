using UnityEngine;

public class TableInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    public override void Interact()
    {
        cameraStateMachine.StartInspect(inspectView);
    }
}