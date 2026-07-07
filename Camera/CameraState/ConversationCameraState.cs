using UnityEngine;

public class ConversationCameraState : CameraState
{
    private Transform returnPoint;

    private bool isConfigured = false;

    public ConversationCameraState(
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
            Transition = stateMachine.ConversationTransitionOut,
            CameraDestination = returnPoint,
            FOVDestination = -1.0f,
            NextState = stateMachine.Conversation,
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
            NextState = stateMachine.FPS,
            OnComplete = () => { stateMachine.Gameplay.ChangeState(stateMachine.Gameplay.FPS); }
        };

        stateMachine.CameraTransition.Configure(request);
        stateMachine.ChangeState(stateMachine.CameraTransition);
    }
}