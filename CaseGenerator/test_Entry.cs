using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class test_Entry : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CaseBuilder cbuild = new CaseBuilder();

        Array levels = EnumExtensions.GetArray<DifficultyLevel>();
        CaseData cd = cbuild.Build(DifficultyLevel.Easy);
        //cd.PrintCaseData();
        //foreach (DifficultyLevel level in levels) {
        //    CaseData cd = cbuild.Build(level);
        //    cd.PrintCaseData();
        //    Debug.Log("-----");
        //}


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
