using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTransitionState : CameraState
{
    private TransitionRequest request;

    private readonly PlayerInteraction interaction;

    private bool isConfigured;

    public CameraTransitionState(
        CameraStateMachine machine,
        PlayerInteraction interaction)
        : base(machine)
    {    
    }

    public void Configure(
        TransitionRequest request)
    {
        this.request = request;
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
        //stateMachine.CameraRig.MoveTo(transitionSettings.Destination, transitionSettings.fov);

        stateMachine.StartTransition(request);
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
        if (!stateMachine.CameraRig.IsMoving)
        {
            //stateMachine.ChangeState(nextState);
            isConfigured = false; // reset for safety
        }
    }

    public override void OnCancel()
    {
    }
}