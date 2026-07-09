using UnityEngine;
using UnityEngine.InputSystem;

public class FPSState : CameraState
{
    private readonly Transform playerHeadCameraPos;
    private readonly MouseLook mouseLook;

    public FPSState(CameraStateMachine machine, Transform playerHeadCameraPos, MouseLook mouseLook) : base(machine)
    {
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

        //stateMachine.CameraRig.SetTarget(playerHeadCameraPos, -1.0f);
        mouseLook.SyncFromCamera(stateMachine.CameraRig.transform);

        //Debug.Log("fps entered");
    }

    public override void Exit()
    {
        stateMachine.DisableAll();
    }

    public override void OnCancel()
    {
    }
}