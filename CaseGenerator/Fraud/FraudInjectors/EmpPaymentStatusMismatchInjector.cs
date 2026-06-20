using System;
using System.Collections.Generic;
using System.Linq;

// Fraud: Invoice date < actual shipping date
public class EmpPaymentStatusMismatchInjector : IFraudInjector
{
    public Contradiction Inject(
        List<Evidence> evidence,
        CaseTruth truth,
        string caseId,
        List<Fact> factsInvolved)
    {
        FactType factTypeMod = FactType.EmpPaymentStatus;
        string contraId = Guid.NewGuid().ToString();
        string contraDisplayId = GenerateDisplayId.generate(EntityType.CONTRA);

        List<Evidence> fraudPair = FraudPair.GetPair(evidence, factTypeMod);

        Evidence firstEv = fraudPair[0];

        Evidence secondEv = fraudPair[1];

        // Get the facts to modify
        Fact firstEvFact = firstEv.Facts.First(x => x.FactType == factTypeMod);

        Fact secondEvFact = secondEv.Facts.First(x => x.FactType == factTypeMod);

        PaymentStatus ps = (PaymentStatus)secondEvFact.Values["EmpPaymentStatus"];

        PaymentStatus newPs = ps;
        // Modify a fact
        do
        {
            newPs = EnumExtensions.GetRandomValue<PaymentStatus>();
        }
        while (newPs == ps);

        firstEvFact.Values["EmpPaymentStatus"] = newPs;

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
            FraudType = FraudType.EmpPaymentStatusMismatch,

            Description = "Employee payment status mismatch."
        };
    }
}