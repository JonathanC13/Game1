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

        // Get evidence
        Evidence invoice =
            evidence.First(
                x =>
                x.Type == EvidenceType.Invoice);

        Evidence shipping =
            evidence.First(
                x =>
                x.Type == EvidenceType.ShippingLog);

        // Get the facts to modify
        Fact invoiceDate =
            invoice.Facts.First(x => x.FactType == factTypeMod);


        Fact shippingDate = shipping.Facts.First( x => x.FactType == factTypeMod);

        DateTime oldDate = (DateTime) shippingDate.Values["Date"];

        // Modify a fact
        shippingDate.Values["Date"] = oldDate.AddDays(UnityEngine.Random.Range(1, 7));

        factsInvolved.Add(invoiceDate);
        factsInvolved.Add(shippingDate);

        return new Contradiction
        {
            Id = contraId,
            DisplayId = contraDisplayId,
            CaseId = caseId,
            FactA = invoiceDate,
            FactB = shippingDate,
            Type = FraudType.ShipmentDateMismatch,

            Description = "Invoice shipment date does not match shipping log"
        };
    }
}