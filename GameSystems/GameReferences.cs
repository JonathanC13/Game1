using UnityEngine;

// Scriptable object assets
[CreateAssetMenu(menuName = "Game/Game References")]
public class GameReferences : ScriptableObject
{
    [Header("Dialogue")]

    [SerializeField]
    private ConversationKeyDatabase conversationKeys;

    [SerializeField]
    private DialogueSettings dialogueSettings;

    [Header("Camera")]

    //[SerializeField]
    //private TransitionLibrary transitionLibrary;

    [Header("Localization")]

    //[SerializeField]
    //private LocalizationDatabase localizationDatabase;

    public ConversationKeyDatabase ConversationKeys => conversationKeys;

    public DialogueSettings DialogueSettings => dialogueSettings;

    //public TransitionLibrary TransitionLibrary => transitionLibrary;

    //public LocalizationDatabase LocalizationDatabase => localizationDatabase;
}