using UnityEngine;
using System;
using System.Diagnostics;

public static class ConditionEvaluator
{
    public static bool Evaluate(
        ConversationContext context,
        ConditionNodeData node)
    {
        switch (node.Key.ValueType)
        {
            case ConversationValueType.Bool:
                return EvaluateBool(context, node);

            case ConversationValueType.Int:
                return EvaluateInt(context, node);

            case ConversationValueType.Float:
                return EvaluateFloat(context, node);

            case ConversationValueType.String:
                return EvaluateString(context, node);

            default:
                UnityEngine.Debug.LogError(
                    $"Unsupported ConversationValueType {node.Key.ValueType}");

                return false;
        }
    }

    private static bool EvaluateBool(
        ConversationContext context,
        ConditionNodeData node)
    {
        bool current =
            context.Get<bool>(node.Key);

        bool compare =
            node.CompareValue.BoolValue;

        return node.Comparison switch
        {
            Comparison.Equals =>
                current == compare,

            Comparison.NotEquals =>
                current != compare,

            _ => false
        };
    }

    private static bool EvaluateInt(
        ConversationContext context,
        ConditionNodeData node)
    {
        int current =
            context.Get<int>(node.Key);

        int compare =
            node.CompareValue.IntValue;

        return node.Comparison switch
        {
            Comparison.Equals =>
                current == compare,

            Comparison.NotEquals =>
                current != compare,

            Comparison.GreaterThan =>
                current > compare,

            Comparison.GreaterOrEqual =>
                current >= compare,

            Comparison.LessThan =>
                current < compare,

            Comparison.LessOrEqual =>
                current <= compare,

            _ => false
        };
    }

    private static bool EvaluateFloat(
        ConversationContext context,
        ConditionNodeData node)
    {
        float current =
            context.Get<float>(node.Key);

        float compare =
            node.CompareValue.FloatValue;

        return node.Comparison switch
        {
            Comparison.Equals =>
                Mathf.Approximately(current, compare),

            Comparison.NotEquals =>
                !Mathf.Approximately(current, compare),

            Comparison.GreaterThan =>
                current > compare,

            Comparison.GreaterOrEqual =>
                current >= compare,

            Comparison.LessThan =>
                current < compare,

            Comparison.LessOrEqual =>
                current <= compare,

            _ => false
        };
    }

    private static bool EvaluateString(
        ConversationContext context,
        ConditionNodeData node)
    {
        string current =
            context.Get<string>(node.Key);

        string compare =
            node.CompareValue.StringValue;

        return node.Comparison switch
        {
            Comparison.Equals =>
                current == compare,

            Comparison.NotEquals =>
                current != compare,

            _ => false
        };
    }
}