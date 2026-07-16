using System;
using UnityEngine;

// The choice and the destination next Dialogue node
[System.Serializable]
public class DialogueChoice
{
    [SerializeField]
    private string id = System.Guid.NewGuid().ToString();

    [SerializeField]
    private string text;

    public string Text
    {
        get => text;
        set => text = value;
    }

    public string Id
    {
        get => id;
    }

    public DialogueChoice(
        string text)
    {
        this.text = text;
    }
}
