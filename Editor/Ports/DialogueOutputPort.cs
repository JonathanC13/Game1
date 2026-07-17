using UnityEditor.Experimental.GraphView;

public sealed class DialogueOutputPort : DialoguePort
{
    public DialogueOutputPort(
        DialogueNodeView nodeView,
        string portId,
        Capacity capacity = Capacity.Single)
        : base(
            nodeView,
            portId,
            Orientation.Horizontal,
            Direction.Output,
            capacity)
    {
    }
}