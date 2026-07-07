public abstract class GameplayState
{
    protected GameplayStateMachine stateMachine;

    protected GameplayState(GameplayStateMachine machine)
    {
        stateMachine = machine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Tick() { }
}