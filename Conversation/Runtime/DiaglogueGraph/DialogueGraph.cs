using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

// Collect the nodes and edges in Lists
[CreateAssetMenu(menuName = "Dialogue/Graph")]
public class DialogueGraph : ScriptableObject
{
    [SerializeReference]
    private List<DialogueNodeData> nodes = new();   // Saved in data, authoritative

    [SerializeField]
    private List<DialogueEdgeData> edges = new();   // Saved in data, authoritative

    [SerializeField]
    private string startGuid;

    // runtime
    private Dictionary<string, DialogueNodeData> nodeLookup;
    private Dictionary<DialogueNodeData, List<RuntimeDialogueEdge>> outgoingEdges;
    private Dictionary<DialogueNodeData, List<RuntimeDialogueEdge>> incomingEdges;


    public IReadOnlyList<DialogueNodeData> Nodes => nodes;
    public IReadOnlyList<DialogueEdgeData> Edges => edges;

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
        string fromPortId)
    {
        return GetOutgoingEdges(node).FirstOrDefault(e => e.FromPortId == fromPortId);
        //return GetOutgoingEdges(node).FirstOrDefault(e => e.Data.EdgeType == edgeType);
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
            DialogueNodeData from = nodeLookup[edge.FromNodeGuid];

            DialogueNodeData to = nodeLookup[edge.ToNodeGuid];

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
        SpeechNodeData node = ScriptableObject.CreateInstance<SpeechNodeData>();

        node.EditorName = "Speech";

        AssetDatabase.AddObjectToAsset(node, this);

        nodes.Add(node);

        return node;
    }

    public ChoiceNodeData CreateChoiceNode(
        string name)
    {
        ChoiceNodeData node = ScriptableObject.CreateInstance<ChoiceNodeData>();

        node.EditorName = "Choice";

        AssetDatabase.AddObjectToAsset(node, this);

        nodes.Add(node);

        return node;
    }

    public ResultNodeData CreateResultNode(
        string name)
    {
        ResultNodeData node = ScriptableObject.CreateInstance<ResultNodeData>();

        node.EditorName = "Result";

        AssetDatabase.AddObjectToAsset(node, this);

        nodes.Add(node);

        return node;
    }

    public ConditionNodeData CreateConditionNode(
        string name)
    {
        ConditionNodeData node = ScriptableObject.CreateInstance<ConditionNodeData>();

        node.EditorName = "Condition";

        AssetDatabase.AddObjectToAsset(node, this);

        nodes.Add(node);

        return node;
    }

    public DialogueEdgeData Connect(
        DialogueNodeData from,
        string fromPortId,
        DialogueNodeData to,
        string toPortId)
    {

        bool exists =
            edges.Any(e =>
                e.FromNodeGuid == from.Guid &&
                e.FromPortId == fromPortId &&
                e.ToNodeGuid == to.Guid &&
                e.ToPortId == toPortId);

        if (exists)
            return null;

        DialogueEdgeData edge =
            new DialogueEdgeData
            {
                FromNodeGuid = from.Guid,

                ToNodeGuid = to.Guid,

                FromPortId = fromPortId,

                ToPortId = toPortId
            };

        edges.Add(edge);

        BuildRuntimeCache();

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif

        return edge;
    }

    public void ConnectTrue(
        ConditionNodeData node,
        DialogueNodeData destination,
        string toPortId)
    {
        Connect(
            node,
            ConditionPorts.True,
            destination,
            toPortId);
    }

    public void ConnectFalse(
        ConditionNodeData node,
        DialogueNodeData destination,
        string toPortId)
    {
        Connect(
            node,
            ConditionPorts.False,
            destination,
            toPortId);
    }

    public void ConnectNext(
        SpeechNodeData node,
        DialogueNodeData destination,
        string toPortId)
    {
        Connect(
            node,
            SpeechPorts.Next,
            destination,
            toPortId);
    }

    public void RemoveEdge(
        DialogueEdgeData edge)
    {
        edges.Remove(edge);

        BuildRuntimeCache();

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    //public void Connect(
    //    DialogueNodeData from,
    //    DialogueNodeData to,
    //    DialogueEdgeType type)
    //{
    //    edges.Add(
    //        new DialogueEdgeData
    //        {
    //            FromGuid = from.Guid,

    //            ToGuid = to.Guid,

    //            EdgeType = type
    //        });
    //}

    //public void ConnectTrue(
    //    ConditionNodeData node,
    //    DialogueNodeData destination)
    //{
    //    edges.Add(
    //        new DialogueEdgeData
    //        {
    //            FromGuid = node.Guid,

    //            ToGuid = destination.Guid,

    //            EdgeType = DialogueEdgeType.True
    //        });
    //}

    //public void ConnectFalse(
    //    ConditionNodeData node,
    //    DialogueNodeData destination)
    //{
    //    edges.Add(
    //        new DialogueEdgeData
    //        {
    //            FromGuid = node.Guid,

    //            ToGuid = destination.Guid,

    //            EdgeType = DialogueEdgeType.False
    //        });
    //}

    //public void AddChoice(
    //    ChoiceNodeData node,
    //    string text,
    //    DialogueNodeData destination)
    //{
    //    edges.Add(
    //        new DialogueEdgeData
    //        {
    //            FromGuid = node.Guid,

    //            ToGuid = destination.Guid,

    //            EdgeType = DialogueEdgeType.Choice,

    //            ChoiceText = text
    //        });
    //}

    public DialogueValidationReport Validate()
    {
        return GraphValidator.Validate(this);
    }


}
