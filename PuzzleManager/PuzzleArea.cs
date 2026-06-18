using UnityEngine;

public class PuzzleArea : MonoBehaviour
{
    public Vector3 size = new Vector3(1f, 0.1f, 1f);

    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    public Quaternion GetSpawnRotation()
    {
        return transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
}