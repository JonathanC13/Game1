using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class DialogueGraphView : GraphView
{
    private DialogueGraph graph;

    private Vector2 pendingNodePosition;
    private DialogueSearchWindow searchWindow;

    public DialogueGraphView(EditorWindow window)
    {
        // custom graph options and add functionality.
        style.flexGrow = 1;

        SetupZoom(
            ContentZoomer.DefaultMinScale,
            ContentZoomer.DefaultMaxScale);

        this.AddManipulator(
            new ContentDragger());

        this.AddManipulator(
            new SelectionDragger());

        this.AddManipulator(
            new RectangleSelector());

        Insert(
            0,
            new GridBackground());

        InitializeSearchWindow();
    }

    private void InitializeSearchWindow()
    {
        searchWindow =
            ScriptableObject.CreateInstance<DialogueSearchWindow>();

        nodeCreationRequest = context =>
        {
            pendingNodePosition =
                contentViewContainer.WorldToLocal(
                    context.screenMousePosition);


            SearchWindow.Open(
                new SearchWindowContext(
                    context.screenMousePosition),
                searchWindow);
        };

        searchWindow.Initialize(
            OnNodeSelected);
    }

    public void Populate(DialogueGraph graph)
    {

        this.graph = graph;

        DeleteElements(graphElements);  // will trigger Node's OnRemovedFromPanel(0

        foreach (DialogueNodeData node in graph.Nodes)
        {
            AddNodeView(CreateNodeView(node));
        }
    }

    private void AddNodeView(
        DialogueNodeView view)
    {

        view.PositionChanged = OnNodeMoved;

        AddElement(view);
    }

    private void DeleteNode(DialogueNodeView view)
    {
        view.Dispose();

        RemoveElement(view);

        graph.RemoveNode(view.NodeData);
    }

    // for existing nodes in graph
    private DialogueNodeView CreateNodeView(DialogueNodeData node)
    {
        DialogueNodeView view = DialogueNodeViewFactory.Create(node);

        view.PositionChanged += OnNodeMoved;

        return view;
    } 

    // create from searchwindow selected.
    private void CreateNodeViewFromDescriptor(DialogueNodeDescriptor descriptor)
    {
        DialogueNodeData node =
            descriptor.Create(graph);

        node.EditorPosition =
            pendingNodePosition;

        DialogueNodeView view =
            DialogueNodeViewFactory.Create(node);

        AddNodeView(view);

        EditorUtility.SetDirty(graph);
    }

    private void OnNodeSelected(
        DialogueNodeDescriptor descriptor)
    {
        CreateNodeViewFromDescriptor(descriptor);
    }

    private void OnNodeMoved(
        DialogueNodeView node)
    {
        EditorUtility.SetDirty(graph);
    }
}