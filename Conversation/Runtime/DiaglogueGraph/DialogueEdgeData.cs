using System;
using UnityEngine;

[Serializable]
public class DialogueEdgeData
{
    [SerializeField]
    private string fromGuid;

    [SerializeField]
    private string toGuid;

    [SerializeField]
    private DialogueEdgeType edgeType;

    [SerializeField]
    private string choiceText;

    public string FromGuid
    {
        get => fromGuid;
        set => fromGuid = value;
    }

    public string ToGuid
    {
        get => toGuid;
        set => toGuid = value;
    }

    public DialogueEdgeType EdgeType
    {
        get => edgeType;
        set => edgeType = value;
    }

    public string ChoiceText
    {
        get => choiceText;
        set => choiceText = value;
    }
}