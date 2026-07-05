using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private CameraStateMachine cameraStateMachine;

    [SerializeField] private DoorInspectInteractable doorInteractable;


    void Awake()
    {
        doorInteractable.OnInteracted += HandleCompleteDoorTransition;
    }

    void Start()
    {
        puzzleManager.BuildPuzzle();
    }

    private void OnDestroy()
    {
        doorInteractable.OnInteracted -= HandleCompleteDoorTransition;
    }

    void HandleCompleteDoorTransition(DoorInspectInteractable door)
    {
        // On Transition complete, call DiagloueManager for conversation with textboxes, on "confirm" calls puzzleManager checkSolution. Cannot back out after "confirm" only click on conversation options.

        // current just check solution with puzzleManager
        puzzleManager.CheckSolution();
    }
}