using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ChoiceNodeData : DialogueNodeData
{
    [SerializeField]
    private string speaker;

    [SerializeField, TextArea]
    private string text;

    [SerializeField]
    private List<DialogueEdgeData> choices = new();

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

    public IReadOnlyList<DialogueEdgeData> Choices => choices;

    //public void AddChoice(
    //    string text,
    //    DialogueNodeData destination)
    //{
    //    choices.Add(
    //        new DialogueChoice(
    //            text,
    //            destination.Guid));
    //}

    public override void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        int choiceCount =
            graph.GetOutgoingEdges(this)
                .Count(e =>
                    e.Data.EdgeType ==
                    DialogueEdgeType.Choice);

        if (choiceCount < 1)
        {
            report.AddError($"Choice node '{this.EditorName}' must have exactly at least one Choice edge. Currently has {choiceCount}.");
        }
    }

    public override void Enter(
        IConversationRunner runner)
    {
        runner.ShowChoices(this);
    }
}