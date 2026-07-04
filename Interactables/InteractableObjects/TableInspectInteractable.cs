using UnityEngine;

public class TableInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    [SerializeField] private TransitionAsset tableTransition;

    public override void Interact()
    {
        //cameraStateMachine.CameraTransition.Configure(
        //    new CameraTransitionSettings
        //    {
        //        Destination = inspectView,
        //        fov = cameraStateMachine.inspectFOV,
        //        NextState = cameraStateMachine.Inspecting,
        //        Fade = false
        //    });


        TransitionRequest request = new()
        {
            Transition = tableTransition,
            CameraDestination = inspectView,
            FOVDestination = cameraStateMachine.inspectFOV,
            NextState = cameraStateMachine.Inspecting
        };

        cameraStateMachine.CameraTransition.Configure(request);
        cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);
    }
}