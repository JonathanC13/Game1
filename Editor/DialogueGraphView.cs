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

        graphViewChanged += OnGraphViewChanged; // GraphView calls this delegate

        RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
    }
    private void OnDetachFromPanel(
        DetachFromPanelEvent evt)
    {
        graphViewChanged -= OnGraphViewChanged;
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

    private GraphViewChange OnGraphViewChanged(
        GraphViewChange change)
    {
        OnEdgesCreated(change);

        OnElementsRemoved(change);

        return change;
    }

    private void OnEdgesCreated(GraphViewChange change)
    {
        if (change.edgesToCreate != null)
        {
            foreach (Edge edge in change.edgesToCreate)
            {
                CreateEdge(edge);
            }
        }
    }

    private void OnElementsRemoved(GraphViewChange change)
    {
        if (change.elementsToRemove != null)
        {
            foreach (GraphElement element in change.elementsToRemove)
            {
                if (element is Edge edge)
                {
                    DeleteEdge(edge);
                }
            }
        }
    }

    private void CreateEdge(Edge edge)
    {
        var output = (DialogueOutputPort)edge.output;

        var input = (DialogueInputPort)edge.input;

        DialogueEdgeData data = 
            graph.Connect(
                output.NodeView.NodeData,
                output.PortId,
                input.NodeView.NodeData,
                input.PortId);

        edge.userData = data;
    }

    private void DeleteEdge(
    Edge edge)
    {
        if (edge.userData is DialogueEdgeData data)
        {
            graph.RemoveEdge(data);
        }
    }
}