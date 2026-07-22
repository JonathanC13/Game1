using System;
using System.Collections.Generic;
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

    public virtual IEnumerable<ValidationResult> Validate(
        ValidationContext context)
    {
        yield break;
    }

    public void InitializeNewGuid()
    {
        guid = System.Guid.NewGuid().ToString();
    }

    public abstract void Enter(IConversationRunner runner);

    //public abstract DialogueEdgeType GetEdgeType(string portId);
}