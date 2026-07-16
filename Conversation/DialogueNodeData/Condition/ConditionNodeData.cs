using System;
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

    public override void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        int trueCount =
            graph.GetOutgoingEdges(this)
                .Count(e =>
                    e.Data.EdgeType ==
                    DialogueEdgeType.True);

        int falseCount =
            graph.GetOutgoingEdges(this)
                .Count(e =>
                    e.Data.EdgeType ==
                    DialogueEdgeType.True);

        if (trueCount != 1)
        {
            report.AddError($"Condition node '{this.EditorName}' must have exactly at least one True edge. Currently has {trueCount}.");
        }

        if (falseCount != 1)
        {
            report.AddError($"Condition node '{this.EditorName}' must have exactly at least one False edge. Currently has {falseCount}.");
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