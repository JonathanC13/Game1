using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private Vector2 pendingNodePosition;
    private Action<DialogueNodeDescriptor> onNodeSelected;

    public void Initialize(Action<DialogueNodeDescriptor> onNodeSelected)
    {
        this.onNodeSelected = onNodeSelected;
    }

    // define what appears
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new();

        tree.Add(
            new SearchTreeGroupEntry(
                new GUIContent("Create Node"),
                0));

        foreach (DialogueNodeDescriptor descriptor
                 in DialogueNodeRegistry.Nodes)
        {
            tree.Add(
                new SearchTreeEntry(
                    new GUIContent(
                        descriptor.MenuName,
                        descriptor.Icon))
                {
                    level = 1,

                    userData = descriptor
                });
        }

        return tree;
    }

    // where creation delegates
    public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
    {
        DialogueNodeDescriptor descriptor =
            entry.userData as DialogueNodeDescriptor;

        onNodeSelected?.Invoke(descriptor);

        return true;
    }
}
