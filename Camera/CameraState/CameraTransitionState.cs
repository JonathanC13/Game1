using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTransitionState : CameraState
{
    private TransitionRequest request;

    private bool isConfigured;

    public CameraTransitionState(
        CameraStateMachine machine)
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
        Debug.Log("enter cameratransition");
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
        Debug.Log("exit cameratransition");
        stateMachine.DisableAll();
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