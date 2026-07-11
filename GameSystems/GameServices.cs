using UnityEngine;

// Runtime Managers
// scenes can have different instances
public class GameServices : MonoBehaviour
{
    public GameplayStateMachine Gameplay;
    public CameraStateMachine Camera;
    public ConversationManager Conversation;
    public PuzzleManager Puzzle;
    // TransitionRunner
}