using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class test_proc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // create evidence
        EvidenceType type = EvidenceType.Invoice;
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(), 
                Type = type
            };
        var archetype = EvidenceRegistry.Archetypes[type];

        // create the required facts
        foreach(var factType in archetype.RequiredFacts)
        {
            Fact fact = FactGenerator.Create(factType);

            fact.EvidenceId = evidence.Id;

            evidence.Facts.Add(fact);
        }
        // create the optional facts
        foreach (var factType in archetype.OptionalFacts)
        {
            bool include = UnityEngine.Random.value > 0.5f;

            if (!include)
            {
                continue;
            }

            Fact fact = FactGenerator.Create(factType);

            fact.EvidenceId = evidence.Id;

            evidence.Facts.Add(fact);
        }

        // generat display content from templates
        evidence.DisplayContent = EvidenceRenderer.Render(evidence);
        Debug.Log(evidence.DisplayContent);

        // rng
        //var values = Enum.GetValues(typeof(EvidenceType));
        //EvidenceType type =
        //    (EvidenceType)
        //        values.GetValue(
        //            UnityEngine.Random.Range(
        //                0,
        //                values.Length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
