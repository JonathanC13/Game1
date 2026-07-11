using UnityEngine;
using System.Collections.Generic;

public class PuzzleResult : MonoBehaviour
{
    public bool IsSolved;

    public List<LinkPair> CorrectLinks = new();

    public List<LinkPair> IncorrectLinks = new();

    public List<LinkPair> MissingLinks = new();

    public string FailureReason;
}
