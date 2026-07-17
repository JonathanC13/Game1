using UnityEditor.Experimental.GraphView;

public sealed class DialogueInputPort : DialoguePort
{
    public DialogueInputPort(
        DialogueNodeView nodeView,
        string portId,
        Capacity capacity = Capacity.Multi)
        : base(
            nodeView,
            portId,
            Orientation.Horizontal,
            Direction.Input,
            capacity)
    {
    }
}