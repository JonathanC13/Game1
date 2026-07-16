using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpeechNodeView : DialogueNodeView
{
    public SpeechNodeView(SpeechNodeData node) : base(node)
    {
    }

    protected override IEnumerable<PortDefinition> GetPorts()
    {
        yield return new PortDefinition(
            SpeechPorts.Next,
            "Next",
            Direction.Output);
    }
}
