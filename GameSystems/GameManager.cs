using UnityEngine;

//Initial game startup
//Saving/loading/autosave
//Level transitions
//Global game state (Main Menu, Playing, Paused)
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameServices gameServices;

    //[SerializeField] private DoorInspectInteractable doorInteractable;


    void Awake()
    {
        //doorInteractable.OnInteracted += HandleCompleteDoorTransition;
    }

    void Start()
    {
        gameServices.Puzzle.BuildPuzzle();
    }

    private void OnDestroy()
    {
        //doorInteractable.OnInteracted -= HandleCompleteDoorTransition;
    }

}