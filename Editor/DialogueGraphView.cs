using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static PlasticGui.Configuration.CloudEdition.Welcome.ValidateEmailAndPassword;

// Manage the GraphView, reference to the data in DialogueGraph and manage Graph elements like Node, edges.
public class DialogueGraphView : GraphView
{
    private DialogueGraph graph;

    private Vector2 pendingNodePosition;
    private DialogueSearchWindow searchWindow;

    // lookup for nodes in the graph
    private readonly Dictionary<string, DialogueNodeView> nodeViews = new();
    // lookup for ports
    private readonly Dictionary<string, DialoguePort> nodePorts = new();

    public event System.Action GraphStructureChanged;

    public event System.Action GraphLayoutChanged;

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

        // create visuals
        foreach (DialogueNodeData node in graph.Nodes)
        {
            CreateNodeView(node);
        }

        // build nodes and edge
        foreach (DialogueEdgeData edges in graph.Edges)
        {
            CreateVisualEdge(edges);
        }
    }

    private void CreateVisualEdge(DialogueEdgeData edgeData)
    {
        if (!nodeViews.TryGetValue(edgeData.FromNodeGuid, out DialogueNodeView fromView))
        {
            return;
        }

        if (!nodeViews.TryGetValue(edgeData.ToNodeGuid, out DialogueNodeView toView)) 
        {
            return;
        }

        DialoguePort output = fromView.GetOutputPort(edgeData.FromPortId);
        DialoguePort input = toView.GetOutputPort(edgeData.ToPortId);

        if (output == null || input == null)
        {
            return;
        }

        Edge edge = new Edge
        {
            output = output,
            input = input,
            userData = edgeData
        };

        edge.output.Connect(edge);
        edge.input.Connect(edge);

        AddElement(edge);
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

        nodeViews[node.Guid] = view;

        AddNodeView(view);

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

        nodeViews[node.Guid] = view;

        AddNodeView(view);

        EditorUtility.SetDirty(graph);

        GraphStructureChanged?.Invoke();    // invoke in CreateNodeViewFromDescriptor due to direct user input. Not in CreateNodeView since it is run for intial build.
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
        // only need validator run when structure change like created edge, remove edge, and remove node.
        // For create node, it invoke in CreateNodeViewFromDescriptor
        bool graphStructureChanged =
            (change.edgesToCreate?.Count ?? 0) > 0 ||
            (change.elementsToRemove?.Count ?? 0) > 0;

        HandleEdgeCreation(change);

        HandleRemoveElements(change);

        HandleMovedElements(change);

        if (graphStructureChanged)
        {
            GraphStructureChanged?.Invoke();
        }

        return change;
    }

    private bool HandleEdgeCreation(
        GraphViewChange change)
    {
        if (change.edgesToCreate == null)
            return false;

        foreach (Edge edge in change.edgesToCreate)
        {
            CreateEdge(edge);
        }

        return change.edgesToCreate.Count > 0;
    }

    private bool HandleRemoveElements(
        GraphViewChange change)
    {
        if (change.elementsToRemove == null)
            return false;

        foreach (GraphElement element in change.elementsToRemove)
        {
            if (element is Edge edge)
            {
                DeleteEdge(edge);
            }

            if (element is DialogueNodeView nodeView)
            {
                graph.RemoveNode(nodeView.NodeData);
            }
        }

        return change.elementsToRemove.Count > 0;
    }

    private void HandleMovedElements(GraphViewChange change)
    {
        if (change.movedElements == null)
            return;

        foreach (GraphElement element in change.movedElements)
        {
            if (element is DialogueNodeView nodeView)
            {
                Rect rect = nodeView.GetPosition();

                nodeView.NodeData.EditorPosition = rect.position;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(graph);
#endif
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

    public DialoguePort GetPort(
        string portId)
    {
        return nodePorts.TryGetValue(
            portId, 
            out DialoguePort port)
                ? port 
                : null;
    }

}