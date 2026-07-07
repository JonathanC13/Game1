public class FPSGameplayState : GameplayState
{
    public FPSGameplayState(GameplayStateMachine machine)
        : base(machine)
    {
    }

    public override void Enter()
    {
        stateMachine.EnablePlayerMovement();
        stateMachine.EnablePlayerInteraction();
    }

    public override void Exit()
    {
        stateMachine.DisableAll();
    }
}