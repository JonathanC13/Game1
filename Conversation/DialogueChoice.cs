using UnityEngine;

// The choice and the destination next Dialogue node
[System.Serializable]
public class DialogueChoice
{
    [SerializeField]
    private string text;

    [SerializeField]
    private string nextGuid;

    public string Text
    {
        get => text;
        set => text = value;
    }

    public string NextGuid => nextGuid;

    public DialogueChoice(
        string text,
        string nextGuid)
    {
        this.text = text;
        this.nextGuid = nextGuid;
    }
}
