using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubmitPuzzleCoordinator : GameBehaviour
{
    [SerializeField] private GameServices gameServices;

    public static SubmitPuzzleCoordinator Instance { get; private set; }
    public static bool HasInstance => Instance != null;

    private PuzzleResult currentResult;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set this specific object as the global Instance
            DontDestroyOnLoad(gameObject); // Keep it alive between scenes
        }
        else
        {
            Destroy(gameObject); // Delete duplicates if a new scene loads one
        }
    }

    public void Register(SubmitPuzzleInteractable submit)
    {
        submit.OnInteracted += HandleSubmitInteracted;
    }

    public void Unregister(SubmitPuzzleInteractable submit)
    {
        submit.OnInteracted -= HandleSubmitInteracted;
    }

    private void HandleSubmitInteracted(SubmitPuzzleInteractable submit)
    {
        // Handle Camera state
        TransitionRequest request = BuildTransitionRequestFadeOut(submit);
        gameServices.Camera.CameraTransition.Configure(request);
        gameServices.Camera.ChangeState(gameServices.Camera.CameraTransition);

        // Handle Gameplay state
        gameServices.Gameplay.ChangeState(gameServices.Gameplay.Blocked);
    }

    // Handle Camera state request
    private TransitionRequest BuildTransitionRequestFadeOut(
        SubmitPuzzleInteractable submit)
    {
        return new TransitionRequest
        {
            Transition = submit.TransitionOut,
            CameraDestination = submit.InspectView,
            FOVDestination = gameServices.Camera.inspectFOV,
            NextState = gameServices.Camera.CameraScripted,
            OnComplete = () => { HandleTransitionToConversation(submit); }
        };
    }

    private void HandleTransitionToConversation(SubmitPuzzleInteractable submit)
    {
        gameServices.Camera.Conversation.Configure(submit.InspectView);

        // Snap to dialogue location for door
        TransitionRequest request = new()
        {
            Transition = submit.ConversationTransitionIn,
            CameraSource = submit.InspectView,
            CameraDestination = submit.DoorConversationView,
            FOVDestination = gameServices.Camera.inspectFOV,
            NextState = gameServices.Camera.Conversation,
            OnComplete = () => { HandleStartConversation(submit); }
        };

        gameServices.Camera.CameraTransition.Configure(request);
        gameServices.Camera.ChangeState(gameServices.Camera.CameraTransition);
    }

    private void HandleStartConversation(SubmitPuzzleInteractable submit)
    {
        gameServices.Gameplay.ChangeState(gameServices.Gameplay.Conversation);

        ConversationContext context = new ConversationContext();
        currentResult = gameServices.Puzzle.EvaluateSolution();
        context.Set(references.ConversationKeys.PuzzleSolved, currentResult.IsSolved);

        ConversationRequest request = new();
        request.Context = context;
        request.Graph = submit.SubmitDialogue;
        request.OnFinished = result =>
        {
            HandleConversationFinished(submit, result);
        };

        gameServices.Conversation.StartConversation(request);
    }

    private void HandleConversationFinished(SubmitPuzzleInteractable submit, ConversationResult result)
    {
        Debug.Log("convo finished: " + result);

        gameServices.Gameplay.ChangeState(gameServices.Gameplay.Blocked);

        // fade out, snap to return point
        TransitionRequest request = new()
        {
            Transition = submit.ConversationTransitionOut,
            CameraDestination = submit.InspectView,
            FOVDestination = -1.0f,
            NextState = gameServices.Camera.Conversation,
            OnComplete = () => { 
                gameServices.Camera.ReturnToPlayer(
                () => {
                    gameServices.Gameplay.ChangeState(gameServices.Gameplay.FPS);
                }); 
            }
        };

        gameServices.Camera.CameraTransition.Configure(request);
        gameServices.Camera.ChangeState(gameServices.Camera.CameraTransition);






        // later when depend on result.
        //switch (result)
        //{
        //    case ConversationResult.LeaveDoor:

        //        TransitionRequest transition = BuildTransition(door);

        //        cameraStateMachine.StartTransition(transition);

        //        break;

        //    case ConversationResult.ReviewEvidence:

        //        gameplayStateMachine.ChangeState(
        //            gameplayStateMachine.Inspect);

        //        break;
        //}
    }

    
}
