using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Manage the DialogueUI
public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text speakerText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] DialogueChoiceButton[] buttons;

    public IReadOnlyList<DialogueChoiceButton> Buttons => buttons;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetSpeaker(string speaker)
    {
        speakerText.text = speaker;
    }

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

}
