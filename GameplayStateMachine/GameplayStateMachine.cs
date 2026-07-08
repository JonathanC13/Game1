using Unity.VisualScripting;
using UnityEngine;

public class GameplayStateMachine : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private InspectObjectController inspectObjectController;
    [SerializeField] private ConversationManager conversationManager;

    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerInteraction PlayerInteraction => playerInteraction;
    public InspectObjectController InspectObjectController => inspectObjectController;
    public ConversationManager ConversationManager => conversationManager;

    public FPSGameplayState FPS { get; private set; }
    public InspectingGameplayState Inspect { get; private set; }
    public ConversationGameplayState Conversation { get; private set; }
    public BlockedGameplayState Blocked { get; private set; }

    private GameplayState currentState;

    private void Awake()
    {
        FPS = new FPSGameplayState(this);
        Inspect = new InspectingGameplayState(this);
        Conversation = new ConversationGameplayState(this);
        Blocked = new BlockedGameplayState(this);

        ChangeState(FPS);
    }

    public void ChangeState(GameplayState next)
    {
        currentState?.Exit();

        currentState = next;

        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Tick();
    }

    public void DisableAll()
    {
        DisablePlayerMovement();
        DisablePlayerInteraction();
        DisableInspectController();
        //HideConverstationManager();
    }

    public void EnablePlayerMovement()
    {
        PlayerMovement.Enable();
    }

    public void DisablePlayerMovement()
    {
        PlayerMovement.Disable();
    }

    public void EnablePlayerInteraction()
    {
        PlayerInteraction.Enable();
    }

    public void DisablePlayerInteraction()
    {
        PlayerInteraction.Disable();
    }

    public void EnableInspectController()
    {
        InspectObjectController.Enable();
    }

    public void DisableInspectController()
    {
        InspectObjectController.Disable();
    }

    //public void ShowConverstationManager()
    //{
    //    ConversationManager.Show();
    //}

    //public void HideConverstationManager()
    //{
    //    ConversationManager.Hide();
    //}

}