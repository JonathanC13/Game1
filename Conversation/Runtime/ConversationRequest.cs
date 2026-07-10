using System;
using UnityEngine;

// The data used in the Conversation.
public class ConversationRequest
{
    public DialogueGraph Graph;

    public ConversationContext Context;

    public Action<ConversationResult> OnFinished;   // Raise event when finished.

    public bool CanCancel = false;
}
