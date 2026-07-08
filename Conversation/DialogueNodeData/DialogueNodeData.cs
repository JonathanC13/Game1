using System;
using UnityEngine;

[Serializable]
public abstract class DialogueNodeData
{
    [SerializeField, HideInInspector]
    private string guid = System.Guid.NewGuid().ToString();

    [SerializeField]
    private string editorName = "Node";

    public string Guid => guid;

    public string EditorName
    {
        get => editorName;
        set => editorName = value;
    }

    public abstract void Enter(IConversationRunner runner);
}