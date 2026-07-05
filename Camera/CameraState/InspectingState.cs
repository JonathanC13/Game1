using UnityEngine;
using UnityEngine.InputSystem;

public class InspectingState : CameraState
{
    private readonly LinkPairManager linkPairManager;

    public InspectingState(CameraStateMachine machine, LinkPairManager linkPairManager) : base(machine)
    {
        this.linkPairManager = linkPairManager;
    }

    public override void Tick()
    {
    }

    public override void Enter()
    {
        Debug.Log("enter inspecting");
        stateMachine.DisableCursorLook();
        stateMachine.ShowCursor();
        stateMachine.DisableMovement();

        stateMachine.DisablePlayerInteraction();
        stateMachine.EnableInspectController();
    }

    public override void Exit()
    {
        Debug.Log("exit inspecting");
        stateMachine.DisableAll();

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