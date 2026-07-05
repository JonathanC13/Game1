using UnityEngine;

public class DialogueCameraState : CameraState
{
    private Transform returnPoint;

    private bool isConfigured = false;

    public DialogueCameraState(
        CameraStateMachine machine)
        : base(machine)
    {
    }

    public void Configure(Transform returnPoint)
    {
        this.returnPoint = returnPoint;
        isConfigured = true;
    }

    public override void Enter()
    {
        stateMachine.DisableCursorLook();
        stateMachine.ShowCursor();
        stateMachine.DisableMovement();

        stateMachine.DisablePlayerInteraction();
        stateMachine.EnableInspectController();
    }

    public override void Exit()
    {
        stateMachine.DisableAll();
        isConfigured = false; // OnCancel already grabbed the value before Exit().
    }

    public override void Tick()
    {
    }

    // currently listen to cancel (esc), later only way out is an explicity option to leave.
    public override void OnCancel()
    {
        if (!isConfigured)
        {
            Debug.LogError("DialogueCameraState returned used without Configure()");
            return;
        }

        // fade out, snap to return point
        TransitionRequest request = new()
        {
            Transition = stateMachine.DialogueTransitionOut,
            CameraDestination = returnPoint,
            FOVDestination = -1.0f,
            NextState = stateMachine.Dialogue,
            OnComplete = () => { ReturnToPlayer(); }
        };

        stateMachine.CameraTransition.Configure(request);
        stateMachine.ChangeState(stateMachine.CameraTransition);
    }

    public void ReturnToPlayer()
    {
        TransitionRequest request = new()
        {
            Transition = stateMachine.ReturnTransitionFadeIn,
            CameraDestination = stateMachine.CameraRig.PlayerHeadPos,
            FOVDestination = -1.0f,
            NextState = stateMachine.FPS
        };

        stateMachine.CameraTransition.Configure(request);
        stateMachine.ChangeState(stateMachine.CameraTransition);
    }
}