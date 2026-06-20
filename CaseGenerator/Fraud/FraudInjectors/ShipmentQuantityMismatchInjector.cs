using System;
using System.Collections.Generic;
using System.Linq;

// Fraud: Invoice date < actual shipping date
public class ShipmentQuantityMismatchInjector : IFraudInjector
{
    public Contradiction Inject(
        List<Evidence> evidence,
        CaseTruth truth,
        string caseId,
        List<Fact> factsInvolved)
    {
        FactType factTypeMod = FactType.ShipmentQuantity;
        string contraId = Guid.NewGuid().ToString();
        string contraDisplayId = GenerateDisplayId.generate(EntityType.CONTRA);

        List<Evidence> fraudPair = FraudPair.GetPair(evidence, factTypeMod);

        Evidence firstEv = fraudPair[0];

        Evidence secondEv = fraudPair[1];

        // Get the facts to modify
        Fact firstEvFact = firstEv.Facts.First(x => x.FactType == factTypeMod);

        Fact secondEvFact = secondEv.Facts.First(x => x.FactType == factTypeMod);

        // Modify a fact
        if (decimal.TryParse(secondEvFact.Values["ShipmentQuantity"].ToString(), out decimal result))
        {
            // Conversion succeeded, use 'result' here
            Random random = new Random();
            firstEvFact.Values["ShipmentQuantity"] = Math.Round(result + random.Next(10, 1000), 0).ToString();
        }
        else
        {
            firstEvFact.Values["ShipmentQuantity"] = "0";
        }

        factsInvolved.Add(firstEvFact);
        factsInvolved.Add(secondEvFact);

        return new Contradiction
        {
            Id = contraId,
            DisplayId = contraDisplayId,
            CaseId = caseId,
            EvidenceA = firstEv,
            EvidenceB = secondEv,
            FactAModded = firstEvFact,
            FactB = secondEvFact,
            FactType = factTypeMod,
            FraudType = FraudType.ShipmentQuantityMismatch,

            Description = "Shipment quantity mismatch."
        };
    }
}