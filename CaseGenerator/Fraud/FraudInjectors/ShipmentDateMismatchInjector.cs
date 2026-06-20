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
        FactType factTypeMod = FactType.ShipmentDate;
        string contraId = Guid.NewGuid().ToString();
        string contraDisplayId = GenerateDisplayId.generate(EntityType.CONTRA);

        List<Evidence> fraudPair = FraudPair.GetPair(evidence, factTypeMod);

        Evidence firstEv = fraudPair[0];

        Evidence secondEv = fraudPair[1];

        // Get the facts to modify
        Fact firstEvDate = firstEv.Facts.First(x => x.FactType == factTypeMod);

        Fact secondEvDate = secondEv.Facts.First(x => x.FactType == factTypeMod);

        DateTime currDate = (DateTime)secondEvDate.Values["ShipmentDate"];

        // Modify a fact
        firstEvDate.Values["ShipmentDate"] = currDate.AddDays(UnityEngine.Random.Range(1, 7));

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
            FraudType = FraudType.ShipmentDateMismatch,

            Description = "Shipment date mismatch."
        };
    }
}