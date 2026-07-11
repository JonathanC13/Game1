using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour
{
    [SerializeField]
    protected GameReferences references;    // Reusable across all scenes
}