using System.Collections.Generic;

public class ContradictionIndex
{
    private readonly
        HashSet<string> _pairs =
            new();

    public void Add(string factA, string factB)
    {
        _pairs.Add(
            Normalize(
                factA,
                factB));
    }

    public bool Contains(string factA, string factB)
    {
        return _pairs.Contains(
            Normalize(
                factA,
                factB));
    }

    private string Normalize(string a, string b)
    {
        return string.Compare(a, b)
            < 0
            ? $"{a}|{b}"
            : $"{b}|{a}";
    }
}