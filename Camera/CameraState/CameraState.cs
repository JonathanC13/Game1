using UnityEngine;

public abstract class CameraState
{
    protected readonly CameraStateMachine stateMachine;
    protected readonly CameraRig cameraRig;

    protected CameraState(CameraStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Tick() { }

    public virtual void LateTick() { }

    public virtual void OnCancel() { }
}