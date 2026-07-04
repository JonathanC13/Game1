using UnityEngine;
using UnityEngine.InputSystem;

public class InspectingState : CameraState
{
    private readonly LinkPairManager linkPairManager;
    private readonly PlayerInteraction interaction;
    private readonly InspectObjectController inspectController;

    public InspectingState(CameraStateMachine machine, LinkPairManager linkPairManager, PlayerInteraction interaction, InspectObjectController inspectController) : base(machine)
    {
        this.linkPairManager = linkPairManager;
        this.interaction = interaction;
        this.inspectController = inspectController;
    }

    public override void Tick()
    {
    }

    public override void Enter()
    {
        stateMachine.DisableCursorLook();
        stateMachine.ShowCursor();
        stateMachine.DisableMovement();

        inspectController.Enable();
    }

    public override void Exit()
    {
        stateMachine.DisableCursorLook();
        stateMachine.HideCursor();
        stateMachine.DisableMovement();

        inspectController.Disable();

    }

    // on 'esc'. For dialogue, disable 'esc', want to trigger fade, so use different input.
    public override void OnCancel()
    {
        linkPairManager.RemovePendingLink();

        //stateMachine.CameraTransition.Configure(
        //    new CameraTransitionSettings
        //    {
        //        Destination = stateMachine.CameraRig.PlayerHeadPos,
        //        fov = -1.0f,
        //        NextState = stateMachine.FPS,
        //        Fade = false
        //    });
        //stateMachine.ChangeState(stateMachine.CameraTransition);

        TransitionRequest request = new()
        {
            Transition = stateMachine.ReturnTransition,
            CameraDestination = stateMachine.CameraRig.PlayerHeadPos,
            FOVDestination = -1.0f,
            NextState = stateMachine.FPS
        };

        stateMachine.CameraTransition.Configure(request);
        stateMachine.ChangeState(stateMachine.CameraTransition);
    }
}