using UnityEngine;

public class DoorInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    public event System.Action<DoorInspectInteractable> OnInteracted;

    public override void Interact()
    {
        Debug.Log("Door selected");
        OnInteracted?.Invoke(this);
    }
}
