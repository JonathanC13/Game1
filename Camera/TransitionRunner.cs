using System.Collections;
using UnityEngine;

public class TransitionRunner
{
    private readonly CameraStateMachine stateMachine;

    public TransitionRunner(CameraStateMachine machine)
    {
        stateMachine = machine;
    }

    public IEnumerator Play(
        TransitionRequest request)
    {

        TransitionContext context = new TransitionContext
        {
            CameraRig = stateMachine.CameraRig,
            ScreenFader = stateMachine.ScreenFader,
            MouseLook = stateMachine.MouseLook,
            PlayerMovement = stateMachine.Movement,
            StateMachine = stateMachine
        };

        foreach (var step in request.Transition.steps)
        {
            yield return step.Execute(
                context,
                request);
        }

        request.OnComplete?.Invoke();

        context.StateMachine.ChangeState(
            request.NextState);
    }
}