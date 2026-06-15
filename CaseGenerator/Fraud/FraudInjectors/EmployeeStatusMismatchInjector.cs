using System;
using System.Collections.Generic;
using System.Linq;

// Fraud: Invoice date < actual shipping date
public class EmployeeStatusMismatchInjector : IFraudInjector
{
    public Contradiction Inject(
        List<Evidence> evidence,
        CaseTruth truth,
        string caseId,
        List<Fact> factsInvolved)
    {
        FactType factTypeMod = FactType.EmployeeStatus;
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

        EmployeeStatus es = (EmployeeStatus)secondEvFact.Values["EmployeeStatus"];

        EmployeeStatus newEs = es;
        // Modify a fact
        do
        {
            newEs = EnumExtensions.GetRandomValue<EmployeeStatus>();
        }
        while (newEs == es);

        firstEvFact.Values["EmployeeStatus"] = newEs;

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
            FraudType = FraudType.EmployeeStatusMismatch,

            Description = "Employee status mismatch."
        };
    }
}