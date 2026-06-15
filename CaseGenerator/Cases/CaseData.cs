// Contains all the information for the current Case.
using System.Collections.Generic;

public class CaseData
{
    public string Id;
    public string DisplayId;

    public CaseTruth Truth;

    public DifficultySettings Difficulty;

    public List<Evidence> Evidence = new();

    public List<Fact> Facts = new();

    public List<Contradiction> Contradictions = new();

    public ContradictionIndex ContradictionIndex;

    public List<ContradictionGroup> ContradictionGroups = new();

    public ContradictionGroupIndex ContradictionGroupIndex;
}