using System;
using System.Collections.Generic;
using System.Linq;
using Unity.ProjectAuditor.Editor;
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

    public override IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        foreach (var choice in dialogueChoices) 
        {
            int choiceCount =
                context.Graph.GetOutgoingEdges(this)
                    .Count(e =>
                        e.FromPortId == choice.Id);

            if (choiceCount != 1)
            {
                yield return new ValidationResult(
                    ValidationSeverity.Error,
                    this,
                    $"Choice '{choice.Text}' must have exactly one True edge. Currently has {choiceCount} outgoing edges.");
            }
        }
        
    }

    public override void Enter(
        IConversationRunner runner)
    {
        runner.ShowChoices(this);
    }

    public string GetChoiceText(string id)
    {
        return dialogueChoices.Find(e => e.Id == id).Text;
    }

    //public override DialogueEdgeType GetEdgeType(
    //    string portId)
    //{
    //    return DialogueEdgeType.Choice;
    //}
}