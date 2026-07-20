using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DialogueNodeView : Node
{
    public DialogueNodeData NodeData { get; }

    public Action<DialogueNodeView> PositionChanged;

    protected readonly Dictionary<string, DialogueInputPort> inputPorts = new();

    protected readonly Dictionary<string, DialogueOutputPort> outputPorts = new();

    public DialogueNodeView(DialogueNodeData node)
    {
        RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);

        this.NodeData = node;

        title = node.EditorName;

        SetPosition(
            new Rect(
                node.EditorPosition,
                GetDefaultSize()));

        // ports
        BuildPorts();

        RefreshExpandedState();
        RefreshPorts();
    }

    protected abstract IEnumerable<PortDefinition> GetPorts();

    private void BuildPorts()
    {
        foreach (PortDefinition definition in GetPorts())
        {
            Port port = CreatePort(definition);

            if (definition.Direction == Direction.Input)
                inputContainer.Add(port);
            else
                outputContainer.Add(port);
        }

        RefreshPorts();

        RefreshExpandedState();
    }

    protected DialoguePort CreatePort(
        PortDefinition definition)
    {
        if (definition.Direction == Direction.Input)
        {
            DialogueInputPort port = new DialogueInputPort(
                this,
                definition.PortId,
                definition.Capacity)
            {
                portName = definition.Name
            };

            inputPorts[definition.PortId] = port;

            return port;
        }
        else
        {
            DialogueOutputPort port = new DialogueOutputPort(
                this,
                definition.PortId,
                definition.Capacity)
            {
                portName = definition.Name
            };

            outputPorts[definition.PortId] = port;

            return port;
        }
    }

    // unexpected removal (switching assets, closing windows, rebuilding the graph
    private void OnDetachFromPanel(DetachFromPanelEvent evt)
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        PositionChanged = null;

        UnregisterCallback<DetachFromPanelEvent>(
            OnDetachFromPanel);
    }

    protected virtual Vector2 GetDefaultSize()
    {
        return new Vector2(250, 150);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        NodeData.EditorPosition = newPos.position;

        PositionChanged?.Invoke(this);
    }

    public virtual void Refresh()
    {
        title = NodeData.EditorName;
    }

    protected DialoguePort CreateOutputPort(
        string name,
        string outputGuid)
    {
        var port =
            new DialoguePort(
                this,
                outputGuid,
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Single);

        port.portName = name;

        return port;
    }

    // Context menus
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //evt.menu.AppendAction(
        //    "Delete",
        //    DeleteNode);

        //evt.menu.AppendAction(
        //    "DuplicateNode",
        //    DuplicateNode);
    }

    public DialogueInputPort GetInputPort(string portId)
    {
        if (!inputPorts.TryGetValue(portId, out var port))
        {
            Debug.LogWarning(
                $"Input port '{portId}' not found on node '{NodeData.name}'.");
        }

        return port;
    }
    public DialogueOutputPort GetOutputPort(string portId)
    {
        if (!outputPorts.TryGetValue(portId, out var port))
        {
            Debug.LogWarning(
                $"Input port '{portId}' not found on node '{NodeData.name}'.");
        }

        return port;
    }

    protected void RebuildPorts()
    {
        ClearPorts();

        BuildPorts();

        RefreshExpandedState();

        RefreshPorts();
    }

    private void ClearPorts()
    {
        inputContainer.Clear();

        outputContainer.Clear();

        inputPorts.Clear();

        outputPorts.Clear();
    }
}

//This base class becomes the place to implement:
//automatic saving of EditorPosition
//common styling
//selection handling
//context menus
//copy/paste
//highlighting from validation
//breakpoint/debug visualization (if you ever want it)