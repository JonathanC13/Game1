using System;
using UnityEngine;

[Serializable]
public class ConversationValue
{
    [SerializeField] private ConversationValueType valueType;
    [SerializeField] private bool boolValue;
    [SerializeField] private int intValue;
    [SerializeField] private float floatValue;
    [SerializeField] private string stringValue;

    public ConversationValueType ValueType => valueType;
    public bool BoolValue => boolValue;
    public int IntValue => intValue;
    public float FloatValue => floatValue;
    public string StringValue => stringValue;
}