using System;
using System.Collections.Generic;
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

    public override IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        if (context.Graph.GetOutgoingEdges(this).Any())
        {
            yield return new ValidationResult(
                ValidationSeverity.Error,
                this,
                $"Result node '{this.EditorName}' cannot have outgoing edges.");
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