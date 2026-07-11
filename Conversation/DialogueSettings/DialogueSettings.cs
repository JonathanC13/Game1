using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Settings")]
public class DialogueSettings : ScriptableObject
{
    [SerializeField]
    private float textSpeed = 30f;

    [SerializeField]
    private AudioClip dialogueBlip;

    [SerializeField]
    private bool skipTypingOnClick = true;

    public float TextSpeed => textSpeed;

    public AudioClip DialogueBlip => dialogueBlip;

    public bool SkipTypingOnClick => skipTypingOnClick;
}