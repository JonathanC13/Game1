using System;
using UnityEngine;

public static class DialogueNodeViewFactory
{
    public static DialogueNodeView Create(DialogueNodeData node)
    {
        return node switch
        {
            SpeechNodeData speech => new SpeechNodeView(speech),
            ChoiceNodeData choice => new ChoiceNodeView(choice),
            ConditionNodeData condition => new ConditionNodeView(condition),
            ResultNodeData result => new ResultNodeView(result),
            _ => throw new NotSupportedException($"Missing view in DialogueNodeFactory for node {node.EditorName}")
        };
    }
}
