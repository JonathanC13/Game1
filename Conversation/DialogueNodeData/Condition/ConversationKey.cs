using UnityEngine;

[CreateAssetMenu(
    menuName = "Dialogue/Conversation Key")]
public class ConversationKey : ScriptableObject
{
    [SerializeField]
    private string keyName;

    [SerializeField]
    private ConversationValueType valueType;

    public string KeyName => keyName;

    public ConversationValueType ValueType => valueType;
}