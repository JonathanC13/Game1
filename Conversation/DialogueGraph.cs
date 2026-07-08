using System;
using System.Collections.Generic;
using UnityEngine;

// Collect the nodes in a List then create a lookup for O(1) Time to access next node
[CreateAssetMenu(menuName = "Dialogue/Graph")]
public class DialogueGraph : ScriptableObject
{
    public List<DialogueNodeData> nodes = new();

    public string startGuid;

    private Dictionary<string, DialogueNodeData> lookup;

    public void SetStart(DialogueNodeData node)
    {
        startGuid = node.Guid;
    }

    public string StartGuid => startGuid;

    private void EnsureLookup()
    {
        if (lookup != null)
        {
            return;
        }

        lookup = new();

        foreach (DialogueNodeData node in nodes)
        {
            lookup[node.Guid] = node;
        }
    }

    public DialogueNodeData GetNode(string guid)
    {
        EnsureLookup();

        if (!lookup.TryGetValue(guid, out DialogueNodeData node))
        {
            Debug.LogError("DialogueGraph lookup missing: " +  guid);
        }
        
        return node;
    }


    public SpeechNodeData CreateSpeechNode(
        string name)
    {
        var node = new SpeechNodeData();

        node.EditorName = name;

        nodes.Add(node);

        return node;
    }

    public ChoiceNodeData CreateChoiceNode(
        string name)
    {
        var node = new ChoiceNodeData();

        node.EditorName = name;

        nodes.Add(node);

        return node;
    }

    public ResultNodeData CreateResultNode(
        string name)
    {
        var node = new ResultNodeData();

        node.EditorName = name;

        nodes.Add(node);

        return node;
    }


    [ContextMenu("Generate Test Graph")]
    private void GenerateTestGraph()
    {
        nodes.Clear();

        var speech = CreateSpeechNode("Greeting");
        speech.Speaker = "Guard";
        speech.Text = "Hello detective.";

        var choice = CreateChoiceNode("Decision");
        choice.Speaker = "Guard";
        choice.Text = "Do you want to leave?";

        speech.Connect(choice);

        var leave = CreateResultNode("Leave");
        leave.Result = ConversationResult.Exit;

        var stay = CreateResultNode("Stay");
        stay.Result = ConversationResult.Confirm;

        choice.AddChoice("Leave", leave);
      
        choice.AddChoice("Stay", stay);

        startGuid = speech.Guid;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
