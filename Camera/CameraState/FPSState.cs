using UnityEngine;
using UnityEngine.InputSystem;

public class FPSState : CameraState
{
    private readonly PlayerInteraction interaction;
    private readonly InspectObjectController inspectController;
    private readonly Transform playerHeadCameraPos;
    private readonly MouseLook mouseLook;

    public FPSState(CameraStateMachine machine, PlayerInteraction interaction, InspectObjectController inspectController, Transform playerHeadCameraPos, MouseLook mouseLook) : base(machine)
    {
        this.interaction = interaction;
        this.inspectController = inspectController;
        this.playerHeadCameraPos = playerHeadCameraPos;
        this.mouseLook = mouseLook;
    }

    public override void Tick()
    {
    }

    public override void LateTick()
    {
        // Optional camera-specific logic like sprint
    }

    public override void Enter()
    {
        stateMachine.EnableCursorLook();
        stateMachine.HideCursor();
        stateMachine.EnableMovement();

        interaction.Enable();
        inspectController.Disable();

        //stateMachine.CameraRig.SetTarget(playerHeadCameraPos, -1.0f);
        mouseLook.SyncFromCamera(stateMachine.CameraRig.transform);

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