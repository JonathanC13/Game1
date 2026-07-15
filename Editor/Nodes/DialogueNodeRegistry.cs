using System.Collections.Generic;

public static class DialogueNodeRegistry
{
    public static IReadOnlyList<DialogueNodeDescriptor> Nodes { get; }
        = new List<DialogueNodeDescriptor>
    {
        new DialogueNodeDescriptor(
            "Speech",
            "Dialogue",
            null,
            graph => graph.CreateSpeechNode()),

        new DialogueNodeDescriptor(
            "Choice",
            "Dialogue",
            null,
            graph => graph.CreateChoiceNode("")),

        new DialogueNodeDescriptor(
            "Condition",
            "Logic",
            null,
            graph => graph.CreateConditionNode("")),

        new DialogueNodeDescriptor(
            "Result",
            "Flow",
            null,
            graph => graph.CreateResultNode(""))
    };
}