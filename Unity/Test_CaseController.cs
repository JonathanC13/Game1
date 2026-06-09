using UnityEngine;
using System.Collections.Generic;

public class test_CaseController : MonoBehaviour
{
    public GameObject evidencePrefab;


    void Start()
    {
        CaseData cd = CaseBuilder.Build();

        print(cd.DisplayId);

        foreach (var (prof, truth) in cd.Profiles)
        {
            print(prof.Id + "--------");
        }

        foreach (Evidence item in cd.Evidence)
        {
            print(item.Type + ": " + item.DisplayContent);
            //EvidenceGameObjectGenerator.Create(item, evidencePrefab);
        }

        foreach (Contradiction item in cd.Contradictions)
        {
            print(item.ProfileId + ": " + item.Description);
            //EvidenceGameObjectGenerator.Create(item, evidencePrefab);
        }
    }

    void print(string str)
    {
        Debug.Log(str);

    }
}