using UnityEngine;

public class BlockedGameplayState : GameplayState
{
    public BlockedGameplayState(GameplayStateMachine machine)
        : base(machine)
    {
    }

    public override void Enter()
    {
        stateMachine.DisableAll();
    }

    public override void Exit()
    {
    }
}
