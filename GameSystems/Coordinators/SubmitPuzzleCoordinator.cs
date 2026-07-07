using UnityEngine;

public class SubmitPuzzleCoordinator : MonoBehaviour
{
    [SerializeField] private GameServices gameServices;

    public static SubmitPuzzleCoordinator Instance { get; private set; }
    public static bool HasInstance => Instance != null;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("hi");
            Instance = this; // Set this specific object as the global Instance
            DontDestroyOnLoad(gameObject); // Keep it alive between scenes
        }
        else
        {
            Debug.Log("bya");
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
        Debug.Log("result: " + gameServices.Puzzle.EvaluateSolution());

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
        bool result = gameServices.Puzzle.EvaluateSolution();

        //ConversationRequest request = BuildConversation(door, result);

        //gameServices.Gameplay.StartConversation(request);
    }
}
