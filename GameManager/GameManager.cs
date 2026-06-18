using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PuzzleSpawner puzzleSpawner;

    void Start()
    {
        puzzleSpawner.Spawn(PuzzleLocation.Table);
    }
}