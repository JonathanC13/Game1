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
    private List<DialogueChoice> dialogueChoices = new();

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

    public IReadOnlyList<DialogueChoice> DialogueChoices => dialogueChoices;

    public void AddChoice(
        string text,
        DialogueNodeData destination)
    {
        dialogueChoices.Add(
            new DialogueChoice(
                text));
    }

    public override void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        int choiceCount =
            graph.GetOutgoingEdges(this)
                .Count(e =>
                    e.Data.EdgeType ==
                    DialogueEdgeType.Choice);

        if (choiceCount != dialogueChoices.Count)
        {
            report.AddError($"Choice node '{this.EditorName}' must have exactly {dialogueChoices.Count} Choice edges.");
        }
    }

    public override void Enter(
        IConversationRunner runner)
    {
        runner.ShowChoices(this);
    }

    //public override DialogueEdgeType GetEdgeType(
    //    string portId)
    //{
    //    return DialogueEdgeType.Choice;
    //}
}