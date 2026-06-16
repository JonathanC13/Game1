using System;
using System.Collections.Generic;
using System.Linq;

// Fraud: Invoice date < actual shipping date
public class PaymentStatusMismatchInjector : IFraudInjector
{
    public Contradiction Inject(
        List<Evidence> evidence,
        CaseTruth truth,
        string caseId,
        List<Fact> factsInvolved)
    {
        FactType factTypeMod = FactType.PaymentStatus;
        string contraId = Guid.NewGuid().ToString();
        string contraDisplayId = GenerateDisplayId.generate(EntityType.CONTRA);

        List<Evidence> shuffledEvidence = evidence.OrderBy(x => System.Guid.NewGuid())
                .ToList();

        // Get evidence
        Evidence firstEv =
            evidence.First(
                x =>
                EvidenceTypeFactTypeList.EF_LIST[x.Type].Contains(factTypeMod));

        Evidence secondEv =
            evidence.First(
                x =>
                {
                    return (x != firstEv && EvidenceTypeFactTypeList.EF_LIST[x.Type].Contains(factTypeMod));
                });

        // Get the facts to modify
        Fact firstEvFact = firstEv.Facts.First(x => x.FactType == factTypeMod);

        Fact secondEvFact = secondEv.Facts.First(x => x.FactType == factTypeMod);

        PaymentStatus ps = (PaymentStatus)secondEvFact.Values["PaymentStatus"];

        PaymentStatus newPs = ps;
        // Modify a fact
        do
        {
            newPs = EnumExtensions.GetRandomValue<PaymentStatus>();
        }
        while (newPs == ps);

        firstEvFact.Values["PaymentStatus"] = newPs;

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
            FraudType = FraudType.PaymentStatusMismatch,

            Description = "Sales payment status mismatch."
        };
    }
}