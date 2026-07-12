using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConversationManager : MonoBehaviour, IConversationRunner
{
    [SerializeField] private DialogueUI dialogueUI;

    private ConversationRequest currentRequest;
    private DialogueGraph currentGraph;
    private DialogueNodeData currentNode;

    public bool IsConversationActive => currentRequest != null;

    public ConversationContext Context
    {
        get;
        private set;
    }

    // Events so other systems can listen
    public event System.Action ConversationStarted;

    public event System.Action ConversationEnded;

    public void StartConversation(ConversationRequest request)
    {
        currentRequest = request;

        currentGraph = request.Graph;

        currentNode = currentGraph.StartNode;

        Context = request.Context;

        dialogueUI.Show();

        ExecuteCurrentNode();
    }

    private void HandleContinue()
    {
        if (currentNode is not SpeechNodeData speechNode)
            return;

        RuntimeDialogueEdge edge =
            currentGraph
                .GetOutgoingEdge(currentNode, DialogueEdgeType.Next);

        if (edge == null)
        {
            Finish(ConversationResult.None);
            return;
        }

        currentNode = edge.To;

        ExecuteCurrentNode();
    }

    private void HandleChoiceSelected(RuntimeDialogueEdge edge)
    {
        currentNode = edge.To;

        ExecuteCurrentNode();
    }

    private void ExecuteCurrentNode()
    {
        if (currentNode == null)
        {
            Finish(ConversationResult.None);
            return;
        }

        currentNode.Enter(this);
    }

    public void ShowSpeech(SpeechNodeData node)
    {
        dialogueUI.ShowSpeech(
            node.Speaker,
            node.Text,
            HandleContinue
            );
    }

    public void ShowChoices(ChoiceNodeData node)
    {
        List<DialogueChoiceViewModel> choices = new();

        foreach (RuntimeDialogueEdge edge in currentGraph.GetOutgoingEdges(node))
        {
            if (edge.Data.EdgeType != DialogueEdgeType.Choice)
                continue;

            RuntimeDialogueEdge capturedEdge = edge;

            choices.Add(new DialogueChoiceViewModel
            {
                Text = capturedEdge.Data.ChoiceText,
                OnSelected = () => HandleChoiceSelected(capturedEdge)
            });
        }

        dialogueUI.ShowChoices(
            node.Speaker,
            node.Text,
            choices);
    }

    public void Finish(ConversationResult result)
    {
        dialogueUI.Hide();

        currentRequest.OnFinished?.Invoke(result);

        currentRequest = null;

        currentGraph = null;

        currentNode = null;

        Context = null;
    }

    public void EvaluateCondition(ConditionNodeData node)
    {
        if (node is not ConditionNodeData)
            return;

        bool passed = ConditionEvaluator.Evaluate(Context, node);

        RuntimeDialogueEdge edge =
            currentGraph.GetOutgoingEdge(
                node,
                passed
                    ? DialogueEdgeType.True
                    : DialogueEdgeType.False);

        if (edge == null)
        {
            Debug.LogError($"Condition node '{node.EditorName}' is missing a {passed} edge.");
            Finish(ConversationResult.None);
            return;
        }

        currentNode = edge.To;

        ExecuteCurrentNode();
    }
}
