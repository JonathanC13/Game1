using System;
using Unity.VisualScripting;

[Serializable]
public class ConditionNodeData : DialogueNodeData
{
    public ConversationKey Key;

    public Comparison Comparison;

    public ConversationValue CompareValue;

    //public string TrueGuid;

    //public string FalseGuid;

    public override void Enter(IConversationRunner runner)
    {
        runner.EvaluateCondition(this);
    }
}