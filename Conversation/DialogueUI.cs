using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Manage the DialogueUI
public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text speakerText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] List<DialogueChoiceButton> choiceButtons;
    [SerializeField] DialogueChoiceButton continueButton;

    public IReadOnlyList<DialogueChoiceButton> ChoiceButtons => choiceButtons;

    private void Start()
    {
        Hide();
    }

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

    public void SetText(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public void ShowSpeech(string speaker, string text, Action onContinue)
    {
        SetSpeaker(speaker);
        SetText(text);
        HideChoiceButtons();

        continueButton.Bind(
            "Continue",
            onContinue);
    }

    public void ShowChoices(
        string speaker,
        string text,
        IReadOnlyList<DialogueChoice> choices,
        Action<DialogueChoice> onSelected)
    {
        SetSpeaker(speaker);
        SetText(text);
        continueButton.Hide();

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            if (i >= choices.Count)
            {
                choiceButtons[i].Hide();
                continue;
            }

            DialogueChoice choice = choices[i];

            //choiceButtons[i].Show(choice.Text);

            choiceButtons[i].Bind(
                choice.Text,
                () => onSelected(choice));
        }
    }

    public void HideChoiceButtons()
    {
        for (int i = 0; i < choiceButtons.Count; i ++)
        {
            choiceButtons[i].Hide();
        }
    }
}