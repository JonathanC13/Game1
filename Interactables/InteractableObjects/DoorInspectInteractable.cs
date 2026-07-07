using Unity.VisualScripting;
using UnityEngine;

public class DoorInspectInteractable : Interactable
{
    public Transform inspectView;
    public CameraStateMachine cameraStateMachine;

    [SerializeField] private TransitionAsset doorTransitionOut;
    [SerializeField] private TransitionAsset dialogueTransitionIn;
    [SerializeField] private Transform doorDialogueView;

    //public event System.Action<DoorInspectInteractable> OnInteracted;

    public override void Interact()
    {
        // old
        //cameraStateMachine.CameraTransition.Configure(
        //    new CameraTransitionSettings
        //    {
        //        Destination = inspectView,
        //        fov = cameraStateMachine.inspectFOV,
        //        NextState = cameraStateMachine.Inspecting,
        //        Fade = true,
        //        FadeDuration = 1f
        //    });


        //TransitionRequest request = new()
        //{
        //    Transition = doorTransitionOut,
        //    CameraDestination = inspectView,
        //    FOVDestination = cameraStateMachine.inspectFOV,
        //    NextState = cameraStateMachine.Inspecting,
        //    OnComplete = () => { InvokeInteracted(); }
        //};

        //cameraStateMachine.CameraTransition.Configure(request);
        //cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);
    }

    //public void InvokeInteracted()
    //{
    //    cameraStateMachine.Dialogue.Configure(inspectView);

    //    // Snap to dialogue location for door
    //    TransitionRequest request = new()
    //    {
    //        Transition = dialogueTransitionIn,
    //        CameraSource = inspectView,
    //        CameraDestination = doorDialogueView,
    //        FOVDestination = cameraStateMachine.inspectFOV,
    //        NextState = cameraStateMachine.Dialogue
    //    };

    //    cameraStateMachine.CameraTransition.Configure(request);
    //    cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);

    //    OnInteracted?.Invoke(this);
    //}
}
