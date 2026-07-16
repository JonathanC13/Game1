using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ResultNodeView : DialogueNodeView
{
    public ResultNodeView(ResultNodeData node) : base(node)
    {

    }

    protected override IEnumerable<PortDefinition> GetPorts()
    {
        yield break;
    }
}
