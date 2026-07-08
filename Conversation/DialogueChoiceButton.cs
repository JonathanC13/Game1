using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

// goes on prefab
public class DialogueChoiceButton : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] Button button;

    public Button Button => button;

    public void Setup(string text, Action onClick)
    {
        label.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick());
    }
}