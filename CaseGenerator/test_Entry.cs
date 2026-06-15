using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            print(item.FactType + ": " + item.FactAModded.Id + "|" + item.FactB.Id + ". " + item.FraudType + ": " + item.Description);
            //EvidenceGameObjectGenerator.Create(item, evidencePrefab);
        }

        foreach (ContradictionGroup item in cd.ContradictionGroups)
        {
            print(item.FactType + ": " + item.OutlierFact.Id + "|" + string.Join(", ", item.TrueFacts.Keys) + ". " + item.FraudType);
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

    void TestAllEvidenceGeneration()
    {
        string caseId = Guid.NewGuid().ToString();
        string displayId = GenerateDisplayId.generate(EntityType.CASE);
        CaseTruth truth = new CaseTruth();

        var values = (EvidenceType[])Enum.GetValues(typeof(EvidenceType));
        foreach (EvidenceType et in values)
        {
            Evidence evidence = EvidenceFactory.Create(new EvidenceToGenerate { Purpose = EvidencePurpose.Required, Type = et }, truth, caseId);
            //print evidence
        }
    }

    
}
