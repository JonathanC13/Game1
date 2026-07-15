using UnityEngine;
using System;

public sealed class DialogueNodeDescriptor
{
    public string MenuName { get; }

    public string Category { get; }

    public Texture2D Icon { get; }

    public Func<DialogueGraph, DialogueNodeData> Create { get; }

    public DialogueNodeDescriptor(
        string menuName,
        string category,
        Texture2D icon,
        Func<DialogueGraph, DialogueNodeData> create)
    {
        MenuName = menuName;
        Category = category;
        Icon = icon;
        Create = create;
    }
}