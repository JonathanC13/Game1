using System;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;

public class DialoguePort : Port
{
    public string Id { get; }

    public DialogueNodeView NodeView { get; }

    public DialoguePort(
        DialogueNodeView nodeView,
        string id,
        Orientation orientation,
        Direction direction,
        Capacity capacity,
        Type type)
        : base(orientation, direction, capacity, type)
    {
        NodeView = nodeView;
        Id = id;
    }
}