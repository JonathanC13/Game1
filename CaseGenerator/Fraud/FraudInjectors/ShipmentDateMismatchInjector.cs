using System;
using System.Collections.Generic;
using System.Linq;

// Fraud: Invoice date < actual shipping date
public class ShipmentDateMismatchInjector : IFraudInjector
{
    public Contradiction Inject(
        List<Evidence> evidence, 
        CaseTruth truth, 
        string caseId, 
        List<Fact> factsInvolved)
    {
        FactType factTypeMod = FactType.ShipmnetDate;
        string contraId = Guid.NewGuid().ToString();
        string contraDisplayId = GenerateDisplayId.generate(EntityType.CONTRA);

        List<Evidence> shuffledEvidence = evidence.OrderBy(x => System.Guid.NewGuid())
                .ToList();

        // Get evidence
        Evidence firstEv =
            evidence.First(
                x =>
                EvidenceTypeFactTypeList.EF_LIST[x.Type].Contains(factTypeMod));

        //Evidence secondEv =
        //    evidence.First(
        //        x =>
        //        x.Type == EvidenceType.ShippingLog);
        Evidence secondEv =
            evidence.First(
                x =>
                { 
                    return (x != firstEv && EvidenceTypeFactTypeList.EF_LIST[x.Type].Contains(factTypeMod)); 
                });

        // Get the facts to modify
        Fact firstEvDate = firstEv.Facts.First(x => x.FactType == factTypeMod);


        Fact secondEvDate = secondEv.Facts.First(x => x.FactType == factTypeMod);

        DateTime currDate = (DateTime)firstEvDate.Values["ShipmentDate"];

        // Modify a fact
        secondEvDate.Values["ShipmentDate"] = currDate.AddDays(UnityEngine.Random.Range(1, 7));

        factsInvolved.Add(firstEvDate);
        factsInvolved.Add(secondEvDate);

        return new Contradiction
        {
            Id = contraId,
            DisplayId = contraDisplayId,
            CaseId = caseId,
            EvidenceA = firstEv,
            EvidenceB = secondEv,
            FactAModded = firstEvDate,
            FactB = secondEvDate,
            FactType = factTypeMod,
            FraudType = FraudType.ShipmnetDateMismatch,

            Description = "Shipment date mismatch."
        };
    }
}