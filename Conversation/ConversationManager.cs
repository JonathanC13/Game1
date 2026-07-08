using UnityEngine;

public class ConversationManager : MonoBehaviour, IConversationRunner
{
    [SerializeField] DialogueUI dialogueUI;

    private ConversationRequest request;

    private string currentGuid;

    public void StartConversation(ConversationRequest request)
    {
        this.request = request;

        currentGuid = request.Graph.startGuid;

        dialogueUI.Show();

        request.Graph.GetNode(currentGuid).Enter(this);
    }

    public void ShowSpeech(SpeechNodeData node)
    {
        DialogueChoiceButton choiceButton = dialogueUI.Buttons[0];
        choiceButton.gameObject.SetActive(true);

        choiceButton.Setup("Continue", () =>
        {
            currentGuid = node.NextGuid;
            node.Enter(this);
        });

        HideRemainingButtons(1);
    }

    public void ShowChoices(ChoiceNodeData node)
    {
        for (int i = 0; i < node.Choices.Count; i++)
        {
            DialogueChoiceButton choiceButton = dialogueUI.Buttons[i];
            choiceButton.gameObject.SetActive(true);

            choiceButton.Setup(node.Choices[i].Text, () =>
            {
                currentGuid = node.Choices[i].NextGuid;
                node.Enter(this);
            });

        }

        HideRemainingButtons(node.Choices.Count);
    }

    public void Finish(ConversationResult result)
    {
        dialogueUI.Hide();

        request.OnFinished?.Invoke(result);

        request = null;
    }

    private void HideRemainingButtons(int used)
    {
        for (int i = used; i < dialogueUI.Buttons.Count; i++)
        {
            dialogueUI.Buttons[i].gameObject.SetActive(false);
        }
    }
}
