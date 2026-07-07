using UnityEngine;

public class InspectingGameplayState : GameplayState
{
    public InspectingGameplayState(GameplayStateMachine machine)
        : base(machine)
    {
    }

    public override void Enter()
    {
        stateMachine.DisablePlayerMovement();
        stateMachine.DisablePlayerInteraction();
        stateMachine.EnableInspectController();
    }

    public override void Exit()
    {
        stateMachine.DisableAll();
    }
}
