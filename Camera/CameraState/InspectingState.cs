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

    public override void OnCancel()
    {
        linkPairManager.RemovePendingLink();

        stateMachine.CameraTransition.Configure(
            stateMachine.CameraRig.PlayerHead,
            stateMachine.FPS);

        stateMachine.ChangeState(stateMachine.CameraTransition);
    }
}