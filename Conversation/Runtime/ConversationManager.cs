using UnityEngine;

public class ConversationManager : MonoBehaviour, IConversationRunner
{
    [SerializeField] private DialogueUI dialogueUI;

    private ConversationRequest currentRequest;

    private DialogueGraph currentGraph;

    private string currentGuid;

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

        currentGuid = request.Graph.StartGuid;

        Context = request.Context;

        dialogueUI.Show();

        ExecuteCurrentNode();
    }

    private void HandleContinue()
    {
        if (currentNode is not SpeechNodeData speechNode)
            return;

        currentGuid = speechNode.NextGuid;

        ExecuteCurrentNode();
    }

    private void HandleChoiceSelected(DialogueChoice choice)
    {
        if (currentNode is not ChoiceNodeData)
            return;

        currentGuid = choice.NextGuid;

        ExecuteCurrentNode();
    }

    private void ExecuteCurrentNode()
    {
        currentNode = currentGraph.GetNode(currentGuid);

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
        dialogueUI.ShowChoices(
            node.Speaker,
            node.Text,
            node.Choices,
            HandleChoiceSelected);
    }

    public void Finish(ConversationResult result)
    {
        dialogueUI.Hide();

        currentRequest.OnFinished?.Invoke(result);

        currentRequest = null;

        currentGraph = null;
    }

    public void EvaluateCondition(ConditionNodeData node)
    {
        if (node is not ConditionNodeData)
            return;

        bool passed = ConditionEvaluator.Evaluate(Context, node);

        currentGuid = passed ? node.TrueGuid : node.FalseGuid;

        ExecuteCurrentNode();
    }
}
