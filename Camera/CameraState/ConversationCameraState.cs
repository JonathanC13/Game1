using UnityEngine;

public class ConversationCameraState : CameraState
{
    private Transform returnPoint;

    private bool isConfigured = false;

    public ConversationCameraState(
        CameraStateMachine machine)
        : base(machine)
    {
    }

    public void Configure(Transform returnPoint)
    {
        this.returnPoint = returnPoint;
        isConfigured = true;
    }

    public override void Enter()
    {
        stateMachine.DisableCursorLook();
        stateMachine.ShowCursor();
    }

    public override void Exit()
    {
        stateMachine.DisableAll();
        isConfigured = false; // OnCancel already grabbed the value before Exit().
    }

    public override void Tick()
    {
    }

    public override void OnCancel()
    {
        //if (!isConfigured)
        //{
        //    Debug.LogError("DialogueCameraState returned used without Configure()");
        //    return;
        //}
    }

    
}