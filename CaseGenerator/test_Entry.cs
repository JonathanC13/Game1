using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class test_Entry : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CaseBuilder cbuild = new CaseBuilder();

        CaseData cd = cbuild.Build(DifficultyLevel.Easy);

        print(cd.Id + " : " + cd.DisplayId);

        printDifficulty(cd.Difficulty);

        foreach (Evidence item in cd.Evidence)
        {
            print(item.Type + ": " + item.DisplayContent);
            //EvidenceGameObjectGenerator.Create(item, evidencePrefab);
        }

        foreach (Contradiction item in cd.Contradictions)
        {
            print(item.FactA.Id + "|" + item.FactB.Id + ". " + item.Type + ": " + item.Description);
            //EvidenceGameObjectGenerator.Create(item, evidencePrefab);
        }


        string selectedAId = "a"; // Will be GameObject -> Fact -> Fact.Id
        string selectedBId = "b";
        bool result =
            cd.ContradictionIndex.TryFind(
                selectedAId,
                selectedBId,
                out Contradiction contradiction);

        HashSet<string> solved = new();
        solved.Add(contradiction.Id);

        bool solvedCase =
            solved.Count ==
            cd.Contradictions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void print(string str)
    {
        Debug.Log(str);

    }

    void printDifficulty(DifficultySettings settings)
    {
        string sb = $"Difficulty:\nFraudCount: {settings.FraudCount.ToString()}\nRedHerringCount: {settings.RedHerringCount}\nMinEvidenceCount: {settings.MinimumEvidenceCount}";
        print(sb);
    }
}
