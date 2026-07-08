using System;
using UnityEngine;

[Serializable]
public class ResultNodeData : DialogueNodeData
{
    [SerializeField]
    private ConversationResult result;

    public ConversationResult Result
    {
        get => result;
        set => result = value;
    }

    public override void Enter(
        IConversationRunner runner)
    {
        runner.Finish(result);
    }
}