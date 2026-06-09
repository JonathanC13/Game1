using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Profiling;

// Fraud: Invoice date < actual shipping date
public class ShipmentDateMismatchInjector : IFraudInjector
{
    public Contradiction Inject(List<Evidence> evidence, CaseTruth truth, string profileId)
    {
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
            invoice.Facts.First(
                x =>
                x.FactType ==
                FactType.ShipmentDate);



        Fact shippingDate =
            shipping.Facts.First(
                x =>
                x.FactType ==
                FactType.ShipmentDate);



        DateTime oldDate = (DateTime) shippingDate.Values["Date"];


        // Modify a fact
        shippingDate.Values["Date"] =
            oldDate.AddDays(UnityEngine.Random.Range(1, 7));



        return new Contradiction
        {
            Id = contraId,
            DisplayId = contraDisplayId,
            ProfileId = profileId,
            FactA = invoiceDate.Id,
            FactB = shippingDate.Id,
            Type = FraudType.ShipmentDateMismatch,

            Description = "Invoice shipment date does not match shipping log"
        };
    }
}