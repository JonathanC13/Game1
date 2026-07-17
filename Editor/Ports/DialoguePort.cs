using System;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;

public class DialoguePort : Port
{
    public string PortId { get; }

    public DialogueNodeView NodeView { get; }

    public DialoguePort(
        DialogueNodeView nodeView,
        string portId,
        Orientation orientation,
        Direction direction,
        Capacity capacity)
        : base(orientation, direction, capacity, typeof(bool))
    {
        NodeView = nodeView;
        PortId = portId;
    }
}

// typeof(bool) since don't care about typing.