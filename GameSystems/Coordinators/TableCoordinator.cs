using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TableCoordinator : MonoBehaviour
{
    [SerializeField] private GameServices gameServices;

    // 1. Declare a public static variable of the same class type
    public static TableCoordinator Instance { get; private set; }
    public static bool HasInstance => Instance != null;

    private void Awake()
    {
        // 2. Ensure only one instance ever exists (The Singleton Pattern)
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

    public void Register(TableInspectInteractable table)
    {
        table.OnInteracted += HandleTableInteracted;
    }

    public void Unregister(TableInspectInteractable table)
    {
        table.OnInteracted -= HandleTableInteracted;
    }

    private void HandleTableInteracted(TableInspectInteractable table)
    {
        // Handle Camera state
        TransitionRequest request = BuildTransitionRequest(table);
        gameServices.Camera.CameraTransition.Configure(request);
        gameServices.Camera.ChangeState(gameServices.Camera.CameraTransition);

        // Handle Gameplay state
        gameServices.Gameplay.ChangeState(gameServices.Gameplay.Blocked);
    }

    // Handle Camera state request
    private TransitionRequest BuildTransitionRequest(
        TableInspectInteractable table)
    {
        return new TransitionRequest
        {
            Transition = table.Transition,
            CameraDestination = table.InspectView,
            FOVDestination = gameServices.Camera.inspectFOV,
            NextState = gameServices.Camera.CameraScripted,
            OnComplete = () => { HandleTransitionComplete(); }
        };
    }

    private void HandleTransitionComplete()
    {
        gameServices.Gameplay.ChangeState(gameServices.Gameplay.Inspect);
    }
}
