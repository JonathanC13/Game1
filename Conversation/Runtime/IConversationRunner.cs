public interface IConversationRunner
{
    void ShowSpeech(SpeechNodeData node);
    void ShowChoices(ChoiceNodeData node);
    void EvaluateCondition(ConditionNodeData node);
    void FinishConversation(ConversationResult result);
}