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
            print(item.FactAId + "|" + item.FactBId + ". " + item.Type + ": " + item.Description);
            //EvidenceGameObjectGenerator.Create(item, evidencePrefab);
        }
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
