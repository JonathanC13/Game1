using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Collect the nodes and edges in Lists
[CreateAssetMenu(menuName = "Dialogue/Graph")]
public class DialogueGraph : ScriptableObject
{
    [SerializeReference]
    private List<DialogueNodeData> nodes = new();

    [SerializeField]
    private List<DialogueEdgeData> edges = new();

    [SerializeField]
    private string startGuid;

    private Dictionary<string, DialogueNodeData> nodeLookup;
    private Dictionary<DialogueNodeData, List<RuntimeDialogueEdge>> outgoingEdges;
    private Dictionary<DialogueNodeData, List<RuntimeDialogueEdge>> incomingEdges;

    public string StartGuid => startGuid;

    public DialogueNodeData StartNode
    {
        get
        {
            return GetNode(StartGuid);
        }
    }

    public IEnumerable<RuntimeDialogueEdge> GetIncomingEdges(
        DialogueNodeData node)
    {
        return incomingEdges[node];
    }
    public IEnumerable<RuntimeDialogueEdge> GetOutgoingEdges(
        DialogueNodeData node)
    {
        return outgoingEdges[node];
    }

    public RuntimeDialogueEdge GetOutgoingEdge(
        DialogueNodeData node,
        DialogueEdgeType edgeType)
    {
        return GetOutgoingEdges(node).FirstOrDefault(e => e.Data.EdgeType == edgeType);
    }

    public void SetStart(DialogueNodeData node)
    {
        startGuid = node.Guid;
    }

    //GUID to Node lookup
    //Outgoing edge map
    //Incoming edge map
    //Runtime edge objects
    private void BuildRuntimeCache()
    {
        nodeLookup = new();

        foreach (DialogueNodeData node in nodes)
        {
            nodeLookup[node.Guid] = node;
        }

        outgoingEdges = new();
        incomingEdges = new();

        foreach (DialogueNodeData node in nodes)
        {
            outgoingEdges[node] = new List<RuntimeDialogueEdge>();
            incomingEdges[node] = new List<RuntimeDialogueEdge>();
        }

        foreach (DialogueEdgeData edge in edges)
        {
            DialogueNodeData from = nodeLookup[edge.FromGuid];

            DialogueNodeData to = nodeLookup[edge.ToGuid];

            RuntimeDialogueEdge runtime =
                new RuntimeDialogueEdge(
                    edge,
                    from,
                    to);

            outgoingEdges[from].Add(runtime);

            incomingEdges[to].Add(runtime);
        }
    }

    public DialogueNodeData GetNode(string guid)
    {
        if (nodeLookup == null)
        {
            BuildRuntimeCache();
        }

        if (nodeLookup.TryGetValue(guid, out DialogueNodeData node))
        {
            return node;
        }

        Debug.LogError("DialogueGraph lookup missing: " + guid);
        return null;
        
    }

    public void RemoveNode(DialogueNodeData node)
    {
        if (node.Guid == startGuid)
        {
            startGuid = string.Empty;
        }

        nodeLookup?.Remove(node.Guid);
        nodes.Remove(node);
    }

    public SpeechNodeData CreateSpeechNode()
    {
        SpeechNodeData node = new SpeechNodeData();

        node.EditorName = "Speech";

        nodes.Add(node);

        return node;
    }

    public ChoiceNodeData CreateChoiceNode(
        string name)
    {
        ChoiceNodeData node = new ChoiceNodeData();

        node.EditorName = "Choice";

        nodes.Add(node);

        return node;
    }

    public ResultNodeData CreateResultNode(
        string name)
    {
        ResultNodeData node = new ResultNodeData();

        node.EditorName = "Result";

        nodes.Add(node);

        return node;
    }

    public ConditionNodeData CreateConditionNode(
        string name)
    {
        ConditionNodeData node = new ConditionNodeData();

        node.EditorName = "Condition";

        return node;
    }

    public void Connect(
        DialogueNodeData from,
        DialogueNodeData to,
        DialogueEdgeType type)
    {
        edges.Add(
            new DialogueEdgeData
            {
                FromGuid = from.Guid,

                ToGuid = to.Guid,

                EdgeType = type
            });
    }

    public void ConnectTrue(
        ConditionNodeData node,
        DialogueNodeData destination)
    {
        edges.Add(
            new DialogueEdgeData
            {
                FromGuid = node.Guid,

                ToGuid = destination.Guid,

                EdgeType = DialogueEdgeType.True
            });
    }

    public void ConnectFalse(
        ConditionNodeData node,
        DialogueNodeData destination)
    {
        edges.Add(
            new DialogueEdgeData
            {
                FromGuid = node.Guid,

                ToGuid = destination.Guid,

                EdgeType = DialogueEdgeType.False
            });
    }

    public void AddChoice(
        ChoiceNodeData node,
        string text,
        DialogueNodeData destination)
    {
        edges.Add(
            new DialogueEdgeData
            {
                FromGuid = node.Guid,

                ToGuid = destination.Guid,

                EdgeType = DialogueEdgeType.Choice,

                ChoiceText = text
            });
    }
}
