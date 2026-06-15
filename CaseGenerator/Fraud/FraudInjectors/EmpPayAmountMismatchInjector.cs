using System;
using System.Collections.Generic;
using System.Linq;

// Fraud: Invoice date < actual shipping date
public class EmpPayAmountMismatchInjector : IFraudInjector
{
    public Contradiction Inject(
        List<Evidence> evidence,
        CaseTruth truth,
        string caseId,
        List<Fact> factsInvolved)
    {
        FactType factTypeMod = FactType.EmpPayAmount;
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

        // Modify a fact
        if (decimal.TryParse(secondEvFact.Values["EmpPayAmount"].ToString(), out decimal result))
        {
            // Conversion succeeded, use 'result' here
            firstEvFact.Values["EmpPayAmount"] = Math.Round(result + GenerateRandom.Money(100, 1000), 2).ToString();
        }
        else
        {
            firstEvFact.Values["EmpPayAmount"] = "0";
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
            FraudType = FraudType.EmpPayAmountMismatch,

            Description = "Employee pay amount mismatch."
        };
    }
}