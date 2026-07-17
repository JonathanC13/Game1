using System;
using UnityEngine;

[Serializable]
public class DialogueEdgeData
{
    [SerializeField]
    private string fromNodeGuid;

    [SerializeField]
    private string fromPortId;

    [SerializeField]
    private string toNodeGuid;

    [SerializeField]
    private string toPortId;

    [SerializeField]
    private DialogueEdgeType edgeType;

    [SerializeField]
    private string outputPortIndex;

    public string FromNodeGuid
    {
        get => fromNodeGuid;
        set => fromNodeGuid = value;
    }

    public string FromPortId
    {
        get => fromPortId;
        set => fromPortId = value;
    }

    public string ToNodeGuid
    {
        get => toNodeGuid;
        set => toNodeGuid = value;
    }

    public string ToPortId
    {
        get => toPortId;
        set => toPortId = value;
    }

    public DialogueEdgeType EdgeType
    {
        get => edgeType;
        set => edgeType = value;
    }

    public string OutputPortIndex
    {
        get => outputPortIndex;
        set => outputPortIndex = value;
    }
}