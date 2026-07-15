using System;
using UnityEngine;

[Serializable]
public abstract class DialogueNodeData : ScriptableObject
{
    [SerializeField, HideInInspector]
    private string guid = System.Guid.NewGuid().ToString();

    [SerializeField]
    private string editorName = "Node";

    [SerializeField]
    private Vector2 editorPosition;

    public string Guid => guid;

    public string EditorName
    {
        get => editorName;
        set => editorName = value;
    }

    public Vector2 EditorPosition
    {
        get => editorPosition;
        set => editorPosition = value;
    }

    public abstract void Validate(
        DialogueGraph graph,
        DialogueValidationReport report);

    public void InitializeNewGuid()
    {
        guid = System.Guid.NewGuid().ToString();
    }

    public abstract void Enter(IConversationRunner runner);
}