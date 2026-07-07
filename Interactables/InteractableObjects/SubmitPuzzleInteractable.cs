using UnityEngine;

public class SubmitPuzzleInteractable : Interactable
{
    [Header("Transition")]
    [SerializeField] private TransitionAsset transitionOut;
    [SerializeField] private TransitionAsset conversationTransitionIn;

    [SerializeField] private Transform inspectView;
    [SerializeField] private Transform doorConversationView;

    public TransitionAsset TransitionOut => transitionOut;
    public TransitionAsset ConversationTransitionIn => conversationTransitionIn;
    public Transform InspectView => inspectView;
    public Transform DoorConversationView => doorConversationView;

    public event System.Action<SubmitPuzzleInteractable> OnInteracted;

    private void Start()
    {
        SubmitPuzzleCoordinator.Instance.Register(this);
    }

    private void OnDestroy()
    {
        if (SubmitPuzzleCoordinator.HasInstance)
            SubmitPuzzleCoordinator.Instance.Unregister(this);
    }

    public override void Interact()
    {
        OnInteracted?.Invoke(this);
    }
}
