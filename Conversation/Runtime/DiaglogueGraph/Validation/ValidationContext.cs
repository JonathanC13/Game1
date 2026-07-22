

using System.Collections;

public sealed class ValidationContext
{
    public DialogueGraph Graph { get; }

    public ValidationContext(DialogueGraph graph)
    {
        Graph = graph;
    }
}
