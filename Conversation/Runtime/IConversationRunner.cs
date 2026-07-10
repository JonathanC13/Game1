public interface IConversationRunner
{
    void ShowSpeech(SpeechNodeData node);
    void ShowChoices(ChoiceNodeData node);
    void Finish(ConversationResult result);
}