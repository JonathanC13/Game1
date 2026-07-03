using UnityEngine;
using UnityEngine.InputSystem;

public class FPSState : CameraState
{
    private readonly PlayerInteraction interaction;
    private readonly InspectObjectController inspectController;

    public FPSState(CameraStateMachine machine, PlayerInteraction interaction, InspectObjectController inspectController) : base(machine)
    {
        this.interaction = interaction;
        this.inspectController = inspectController;
    }

    public override void Tick()
    {
    }

    public override void Enter()
    {
        stateMachine.EnableCursorLook();
        stateMachine.HideCursor();
        stateMachine.EnableMovement();

        interaction.Enable();
        inspectController.Disable();

        Debug.Log("fps entered");
    }

    public override void Exit()
    {
        stateMachine.DisableCursorLook();
        stateMachine.HideCursor();
        stateMachine.DisableMovement();

        interaction.Disable();
    }

    public override void OnCancel()
    {
    }
}