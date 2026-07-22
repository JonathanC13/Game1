public enum ValidationSeverity
{
    Info, 
    Warning, 
    Error
}

// Result for the validation.
public sealed class ValidationResult
{
    public ValidationSeverity Severity { get; }

    public DialogueNodeData Node { get; }

    public string Message { get; }

    public ValidationResult(
        ValidationSeverity severity,
        DialogueNodeData node,
        string message)
    {
        Severity = severity;
        Node = node;
        Message = message;
    }
}