using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class DialogueGraphValidator
{
    public readonly List<IGraphValidationRule> rules = new();

    public DialogueGraphValidator()
    {
        rules.Add(new StartNodeValidator());

        rules.Add(new EdgeValidator());

        rules.Add(new ReachabilityValidator());
    }

    public List<ValidationResult> Validate(DialogueGraph graph)
    {
        ValidationContext context = new(graph);

        List<ValidationResult> results = new();

        foreach (IGraphValidationRule rule in rules)
        {
            results.AddRange(rule.Validate(context));
        }

        foreach (DialogueNodeData node in graph.Nodes)
        {
            results.AddRange(node.Validate(context));
        }

        return results;
    }
}

public sealed class StartNodeValidator : IGraphValidationRule
{
    public IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        if (context.Graph.StartNode == null)
        {
            yield return new ValidationResult(
                ValidationSeverity.Error,
                null,
                "No starting node.");
        }
    }
}

public sealed class EdgeValidator : IGraphValidationRule
{
    // Checks every edge references valid nodes
    public IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        foreach (DialogueEdgeData edge in context.Graph.Edges)
        {

            // use GUIDs since if invalid graph, the cache lookup not complete.
            DialogueNodeData fromNode = context.Graph.GetNode(edge.FromNodeGuid);
            DialogueNodeData toNode = context.Graph.GetNode(edge.ToNodeGuid);

            if (fromNode != null && toNode == null)
            {
                yield return new ValidationResult(
                    ValidationSeverity.Warning,
                    fromNode,
                    $"Edge reference missing destination node ({edge.ToNodeGuid})");
            }
            else if (fromNode == null && toNode != null)
            {
                yield return new ValidationResult(
                    ValidationSeverity.Warning,
                    toNode,
                    $"Edge reference missing source node ({edge.FromNodeGuid})");
            }
            else
            {
                yield return new ValidationResult(
                    ValidationSeverity.Warning,
                    toNode,
                    $"Edge reference missing source ({edge.FromNodeGuid}) and desitination node ({edge.ToNodeGuid})");
            }
        }
    }
}

public sealed class ReachabilityValidator : IGraphValidationRule
{
    public IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        if (context.Graph.StartNode == null)
        {
            yield break;
        }

        // validate the runtime cache since this is the data used to traverse the conversation.

        HashSet<DialogueNodeData> visited = new();

        Queue<DialogueNodeData> qu = new();

        qu.Enqueue(context.Graph.StartNode);
        visited.Add(context.Graph.StartNode);

        while (qu.Count > 0)
        {
            var node = qu.Dequeue();

            foreach (RuntimeDialogueEdge edge in context.Graph.GetOutgoingEdges(node))
            {
                if (visited.Contains(edge.To))
                {
                    continue;
                }

                qu.Enqueue(edge.To);
                visited.Add(edge.To);
            }
        }

        foreach (DialogueNodeData node in context.Graph.Nodes)
        {
            if (!visited.Contains(node))
            {
                yield return new ValidationResult(
                    ValidationSeverity.Warning,
                    node,
                    $"Node {node.EditorName} is unreachable.");
            }
        }
    }
}