using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTransitionState : CameraState
{
    private Transform destination;
    private CameraState nextState;

    private readonly PlayerInteraction interaction;

    private bool isConfigured;

    public CameraTransitionState(
        CameraStateMachine machine,
        PlayerInteraction interaction)
        : base(machine)
    {    
    }

    public void Configure(
        Transform destination,
        CameraState nextState)
    {
        this.destination = destination;
        this.nextState = nextState;
        this.isConfigured = true;
    }

    public override void Enter()
    {
        if (!isConfigured)
        {
            Debug.LogError("TransitionState used without Configure()");
            return;
        }

        stateMachine.DisableAll();
        stateMachine.CameraRig.MoveTo(destination);
    }

    public override void Exit()
    {
        stateMachine.DisableCursorLook();
        stateMachine.HideCursor();
        stateMachine.DisableMovement();
        isConfigured = false;
    }

    public override void Tick()
    {
        if (stateMachine.CameraRig.HasReachedTarget())
        {
            stateMachine.ChangeState(nextState);
            isConfigured = false; // reset for safety
        }
    }

    public override void OnCancel()
    {
    }
}