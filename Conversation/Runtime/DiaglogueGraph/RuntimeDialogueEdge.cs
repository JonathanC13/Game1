public sealed class RuntimeDialogueEdge
{
    public DialogueEdgeData Data { get; }

    public DialogueNodeData From { get; }

    public DialogueNodeData To { get; }

    public RuntimeDialogueEdge(
        DialogueEdgeData data,
        DialogueNodeData from,
        DialogueNodeData to)
    {
        Data = data;
        From = from;
        To = to;
    }
}