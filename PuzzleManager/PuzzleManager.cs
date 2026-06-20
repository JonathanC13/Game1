using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePrefab;

    public PuzzleArea tableArea;
    public PuzzleArea playerArea;

    private PuzzleBuilder PuzzleBuilder;

    private void Start()
    {
        //PuzzleBuilder = new PuzzleBuilder();
        //PuzzleBuilder.generate();
    }

    public void MoveTo(PuzzleLocation location)
    {
        Debug.Log(location);
        PuzzleArea target = location == PuzzleLocation.Table
            ? tableArea
            : playerArea;

        GameObject puzzle = Instantiate(
            puzzlePrefab,
            target.GetSpawnPosition(),
            target.GetSpawnRotation()
        );

        puzzle.transform.SetParent(target.transform);
    }
}