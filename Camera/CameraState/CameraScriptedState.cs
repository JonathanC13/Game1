using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScriptedState : CameraState
{
    private readonly LinkPairManager linkPairManager;

    public CameraScriptedState(CameraStateMachine machine, LinkPairManager linkPairManager) : base(machine)
    {
        this.linkPairManager = linkPairManager;
    }

    public override void Tick()
    {
    }

    public override void Enter()
    {
        stateMachine.DisableCursorLook();
        stateMachine.ShowCursor();
    }

    public override void Exit()
    {
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

        stateMachine.Gameplay.ChangeState(stateMachine.Gameplay.Blocked);

        TransitionRequest request = new()
        {
            Transition = stateMachine.ReturnTransition,
            CameraDestination = stateMachine.CameraRig.PlayerHeadPos,
            FOVDestination = -1.0f,
            NextState = stateMachine.FPS,
            OnComplete = () => { stateMachine.Gameplay.ChangeState(stateMachine.Gameplay.FPS); }
        };

        stateMachine.CameraTransition.Configure(request);
        stateMachine.ChangeState(stateMachine.CameraTransition);
    }
}