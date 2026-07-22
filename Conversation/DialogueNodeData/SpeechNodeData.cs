using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SpeechNodeData : DialogueNodeData
{
    [SerializeField]
    private string speaker;

    [SerializeField, TextArea]
    private string text;

    [SerializeField]
    private string nextGuid;

    public string Speaker
    {
        get => speaker;
        set => speaker = value;
    }

    public string Text
    {
        get => text;
        set => text = value;
    }

    //public string NextGuid => nextGuid;

    //public void Connect(DialogueNodeData node)
    //{
    //    nextGuid = node.Guid;
    //}

    public override IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        int nextCount =
            context.Graph.GetOutgoingEdges(this).Count();
                //e =>
                //    e.Data.EdgeType ==
                //    DialogueEdgeType.Next);

        if (nextCount != 1)
        {
            yield return new ValidationResult(
                ValidationSeverity.Error,
                this,
                $"Speech node '{this.EditorName}' must have exactly one Next edge. Currently has {nextCount} outgoing edges.");
        }
    }

    public override void Enter(IConversationRunner runner)
    {
        Debug.Log("speech enter");
        runner.ShowSpeech(this);
    }

    //public override DialogueEdgeType GetEdgeType(
    //    string portId)
    //{
    //    return DialogueEdgeType.Next;
    //}
}