using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ConditionNodeView : DialogueNodeView
{
    public ConditionNodeView(ConditionNodeData node) : base(node)
    {
    }

    protected override IEnumerable<PortDefinition> GetPorts()
    {
        yield return new PortDefinition(
            ConditionPorts.Input,
            "In",
            Direction.Input);

        yield return new PortDefinition(
            "True",
            Guid.NewGuid().ToString(),
            Direction.Output);

        yield return new PortDefinition(
            "False",
            Guid.NewGuid().ToString(),
            Direction.Output);
    }
}
