public class ConversationGameplayState : GameplayState
{
    public ConversationGameplayState(GameplayStateMachine machine)
        : base(machine)
    {
    }

    public override void Enter()
    {
        stateMachine.DisablePlayerMovement();
        stateMachine.DisablePlayerInteraction();
        stateMachine.EnableInspectController();
        //stateMachine.ShowConverstationManager();
    }

    public override void Exit()
    {
        stateMachine.DisableAll();
    }
}