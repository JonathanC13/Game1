using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueValidationReport
{
    public List<string> Errors { get; } = new List<string>();

    public List<string> Warnings { get; } = new List<string>();

    public bool IsValid => Errors.Count == 0;

    public void AddError(string message)
    {
        Errors.Add(message);
    }

    public void AddWarning(string message)
    {
        Warnings.Add(message);
    }
}
