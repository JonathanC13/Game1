using System;
using UnityEditor.Experimental.GraphView;

public sealed class PortDefinition
{
    public string Id { get; }

    public string Name { get; }

    public Direction Direction { get; }

    public Port.Capacity Capacity { get; }

    public PortDefinition(
        string id,
        string name,
        Direction direction = default,
        Port.Capacity capacity = Port.Capacity.Single
        )
    {
        Name = name;
        Id = id;
        Direction = direction;
        Capacity = capacity;
    }
}