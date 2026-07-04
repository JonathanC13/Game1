using Unity.VisualScripting;
using UnityEngine;

public class DoorInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    [SerializeField] private TransitionAsset doorTransition;

    public event System.Action<DoorInspectInteractable> OnInteracted;

    public override void Interact()
    {
        //cameraStateMachine.CameraTransition.Configure(
        //    new CameraTransitionSettings
        //    {
        //        Destination = inspectView,
        //        fov = cameraStateMachine.inspectFOV,
        //        NextState = cameraStateMachine.Inspecting,
        //        Fade = true,
        //        FadeDuration = 1f
        //    });


        TransitionRequest request = new()
        {
            Transition = doorTransition,
            CameraDestination = inspectView,
            FOVDestination = cameraStateMachine.inspectFOV,
            NextState = cameraStateMachine.Inspecting,
            OnComplete = () => { InvokeInteracted(); }
        };

        cameraStateMachine.CameraTransition.Configure(request);
        cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);
    }

    public void InvokeInteracted()
    {
        OnInteracted?.Invoke(this);
    }
}
