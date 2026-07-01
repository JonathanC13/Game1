using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    public DoorInspectInteractable doorInteractable;

    void Awake()
    {
        doorInteractable.OnInteracted += HandleFinDoorInteracted;

        puzzleManager.BuildPuzzle();
    }

    private void OnDestroy()
    {
        doorInteractable.OnInteracted -= HandleFinDoorInteracted;
    }

    void HandleFinDoorInteracted(DoorInspectInteractable door)
    {
        // GameManager listens to call SceneTransitionManager to run animation to zoom, cut to black, cut into looking through peep hole with eye. Call DiagloueManager for conversation with textboxes, on "confirm" calls puzzleManager checkSolution. Cannot back out after "confirm" only click on conversation options.

        // current just check solution with puzzleManager
        puzzleManager.CheckSolution();
    }
}