using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GraphValidator
{
    public static DialogueValidationReport Validate(
        DialogueGraph graph)
    {
        DialogueValidationReport report = new();

        StartNodeValidator.Validate(graph, report);

        EdgeValidator.Validate(graph, report);

        ReachabilityValidator.Validate(graph, report);

        NodeRuleValidator.Validate(graph, report);

        return report;
    }
}

public static class StartNodeValidator
{
    public static void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        if (graph.StartNode == null)
        {
            report.AddError("Dialogue graph has no start node.");
        }
    }
}

public static class EdgeValidator
{
    // Checks every edge references valid nodes
    public static void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        foreach (DialogueEdgeData edge in graph.Edges)
        {
            // use GUIDs since if invalid graph, the cache lookup not complete.
            if (graph.GetNode(edge.FromGuid) == null)
            {
                report.AddError($"Edge references missing source node ({edge.FromGuid}).");
            }

            if (graph.GetNode(edge.ToGuid) == null)
            {
                report.AddError($"Edge references missing destination node ({edge.ToGuid}).");
            }
        }
    }
}

public static class ReachabilityValidator
{
    public static void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        if (graph.StartNode == null)
        {
            return;
        }

        // validate the runtime cache since this is the data used to traverse the conversation.

        HashSet<DialogueNodeData> visited = new();

        Queue<DialogueNodeData> qu = new();

        qu.Enqueue(graph.StartNode);
        visited.Add(graph.StartNode);

        while (qu.Count > 0)
        {
            var node = qu.Dequeue();

            foreach (RuntimeDialogueEdge edge in graph.GetOutgoingEdges(node))
            {
                if (visited.Contains(edge.To))
                {
                    continue;
                }

                qu.Enqueue(edge.To);
                visited.Add(edge.To);
            }
        }

        foreach (DialogueNodeData node in graph.Nodes)
        {
            if (!visited.Contains(node))
            {
                report.AddError($"Node {node.EditorName} is unreachable.");
            }
        }
    }
}


public static class NodeRuleValidator
{
    public static void Validate(
        DialogueGraph graph,
        DialogueValidationReport report)
    {
        // Speech: Has one outgoing Next edge
        // Choice: Has >= 1 outgoing Choice edges
        // Condition: Has 2 edges one True and one False
        // Result: No outgoing edges

        foreach (DialogueNodeData node in graph.Nodes)
        {
            switch (node)
            {
                case SpeechNodeData speech:

                    speech.Validate(graph, report);

                    break;

                case ChoiceNodeData choice:

                    choice.Validate(graph, report);

                    break;

                case ConditionNodeData condition:

                    condition.Validate(graph, report);

                    break;

                case ResultNodeData result:

                    result.Validate(graph, report);

                    break;
            }
        }
    }
}