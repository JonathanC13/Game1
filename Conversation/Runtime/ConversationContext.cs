using UnityEngine;
using System.Collections.Generic;

// Data that every node has access to.
public class ConversationContext
{
    public readonly Dictionary<ConversationKey, object> values = new();

    public void Set<T>(ConversationKey key, T value)
    {
        values[key] = value;
    }

    public T Get<T>(ConversationKey key)
    {
        if(values.TryGetValue(key, out object value))
        {
            return (T)value;
        }

        return default;
    }

    public bool TryGet<T>(ConversationKey key, out T value)
    {
        if (values.TryGetValue(key, out object obj) && obj is T cast)
        {
            value = cast;
            return true;
        }

        value = default;
        return false;
    }

    public bool Has(ConversationKey key)
    {
        return values.ContainsKey(key);
    }

    public bool Remove(ConversationKey key)
    {
        return values.Remove(key);
    }

    public void Clear()
    {
        values.Clear();
    }
}
