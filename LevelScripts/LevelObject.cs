using UnityEngine;

// Attach to GameObject and assign an identifier to it for referencing.
[ExecuteAlways]
public class LevelObject : MonoBehaviour
{
    [SerializeField] private string objectId;

    public string ObjectId => objectId;

    private void Awake()
    {
        EnsureID();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        EnsureID();
    }
#endif

    private void EnsureID()
    {
        if (string.IsNullOrEmpty(objectId))
        {
            objectId = System.Guid.NewGuid().ToString();
        }
    }
}