using System;
using System.Linq;
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

    public override void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        if (graph.GetOutgoingEdges(this).Any())
        {
            report.AddError($"Result node '{this.EditorName}' cannot have outgoing edges.");
        }
    }

    public override void Enter(
        IConversationRunner runner)
    {
        runner.FinishConversation(result);
    }

    //public override DialogueEdgeType GetEdgeType(
    //    string portId)
    //{
    //    return DialogueEdgeType.;
    //}
}