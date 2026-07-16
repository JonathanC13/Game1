using System;
using UnityEngine;

[Serializable]
public class DialogueEdgeData
{
    [SerializeField]
    private string fromGuid;

    [SerializeField]
    private string fromPortId;

    [SerializeField]
    private string toGuid;

    [SerializeField]
    private string toPortId;

    [SerializeField]
    private DialogueEdgeType edgeType;

    [SerializeField]
    private string choiceText;

    [SerializeField]
    private string outputPortIndex;

    public string FromGuid
    {
        get => fromGuid;
        set => fromGuid = value;
    }

    public string FromPortId
    {
        get => fromPortId;
        set => fromPortId = value;
    }

    public string ToGuid
    {
        get => toGuid;
        set => toGuid = value;
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

    // takes from Dialogue choice.
    public string ChoiceText
    {
        get => choiceText;
        set => choiceText = value;
    }

    public string OutputPortIndex
    {
        get => outputPortIndex;
        set => outputPortIndex = value;
    }
}