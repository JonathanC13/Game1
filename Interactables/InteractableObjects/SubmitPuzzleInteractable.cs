using UnityEngine;

public class SubmitPuzzleInteractable : Interactable
{
    [Header("Transition")]
    [SerializeField] private TransitionAsset transitionOut;
    [SerializeField] private TransitionAsset conversationTransitionIn;
    [SerializeField] private TransitionAsset conversationTransitionOut;

    [SerializeField] private Transform inspectView;
    [SerializeField] private Transform doorConversationView;

    [SerializeField] private DialogueGraph submitDialogue;

    public TransitionAsset TransitionOut => transitionOut;
    public TransitionAsset ConversationTransitionIn => conversationTransitionIn;
    public TransitionAsset ConversationTransitionOut => conversationTransitionOut;
    public Transform InspectView => inspectView;
    public Transform DoorConversationView => doorConversationView;
    public DialogueGraph SubmitDialogue => submitDialogue;

    public event System.Action<SubmitPuzzleInteractable> OnInteracted;

    private void Start()
    {
        SubmitPuzzleCoordinator.Instance.Register(this);
;    }

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
