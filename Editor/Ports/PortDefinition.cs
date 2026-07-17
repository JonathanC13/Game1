using System;
using UnityEditor.Experimental.GraphView;

public sealed class PortDefinition
{
    public string PortId { get; }

    public string Name { get; }

    public Direction Direction { get; }

    public Port.Capacity Capacity { get; }

    public PortDefinition(
        string portId,
        string name,
        Direction direction = default,
        Port.Capacity capacity = Port.Capacity.Single
        )
    {
        Name = name;
        PortId = portId;
        Direction = direction;
        Capacity = capacity;
    }
}