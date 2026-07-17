using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChoiceNodeView : DialogueNodeView
{
    public ChoiceNodeView(ChoiceNodeData node) : base(node)
    {
    }

    protected override IEnumerable<PortDefinition> GetPorts()
    {
        yield return new PortDefinition(
            ChoicePorts.Input,
            "In",
            Direction.Input);

        ChoiceNodeData node =
            (ChoiceNodeData)NodeData;

        for (int i = 0; i < node.DialogueChoices.Count; i++)
        {
            yield return new PortDefinition(
                node.DialogueChoices[i].Id,
                node.DialogueChoices[i].Text,
                Direction.Output);
        }
    }
}
