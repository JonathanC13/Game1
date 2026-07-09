using System;
using UnityEngine;

[Serializable]
public class SpeechNodeData : DialogueNodeData
{
    [SerializeField]
    private string speaker;

    [SerializeField, TextArea]
    private string text;

    [SerializeField]
    private string nextGuid;

    public string Speaker
    {
        get => speaker;
        set => speaker = value;
    }

    public string Text
    {
        get => text;
        set => text = value;
    }

    public string NextGuid => nextGuid;

    public void Connect(DialogueNodeData node)
    {
        nextGuid = node.Guid;
    }

    public override void Enter(IConversationRunner runner)
    {
        Debug.Log("speech enter");
        runner.ShowSpeech(this);
    }
}