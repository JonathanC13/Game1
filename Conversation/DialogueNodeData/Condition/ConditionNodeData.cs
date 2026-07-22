using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

[Serializable]
public class ConditionNodeData : DialogueNodeData
{
    public ConversationKey Key;

    public Comparison Comparison;

    public ConversationValue CompareValue;

    //public string TrueGuid;

    //public string FalseGuid;

    public override IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        int trueCount =
            context.Graph.GetOutgoingEdges(this)
                .Count(e =>
                    e.FromPortId == ConditionPorts.True);

        int falseCount =
            context.Graph.GetOutgoingEdges(this)
                .Count(e =>
                    e.FromPortId == ConditionPorts.False);

        if (trueCount != 1)
        {
            yield return new ValidationResult(
                ValidationSeverity.Error,
                this,
                $"Condition node '{this.EditorName}' must have exactly one True edge. Currently has {trueCount} outgoing edges.");
        }

        if (falseCount != 1)
        {
            yield return new ValidationResult(
                ValidationSeverity.Error,
                this,
                $"Condition node '{this.EditorName}' must have exactly one False edge. Currently has {trueCount} outgoing edges.");
        }
    }

    public override void Enter(IConversationRunner runner)
    {
        runner.EvaluateCondition(this);
    }

    //public override DialogueEdgeType GetEdgeType(
    //    string portId)
    //{
    //    if (portId == ConditionPorts.True)
    //        return DialogueEdgeType.True;

    //    return DialogueEdgeType.False;
    //}
}