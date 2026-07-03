using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransitioningSceneState : CameraState
{
    private readonly PlayerInteraction interaction;
    public TransitioningSceneState(CameraStateMachine machine, PlayerInteraction interaction) : base(machine)
    {
        this.interaction = interaction;
    }

    public override void Enter()
    {
        stateMachine.DisableAll();
        //stateMachine.CameraRig.SnapTo(target);
        //stateMachine.CameraRig.MoveTo(target);
    }

    public override void Exit()
    {
    }

    public override void OnCancel()
    {
    }
}