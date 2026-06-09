using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine.Profiling;

public static class CaseBuilder
{
    public static CaseData Build()
    {
        CaseTruth truth = CaseTruthGenerator.Generate();
        Dictionary<CaseProfile, CaseTruth> profileDict = new();
        List<Evidence> evidenceList = new();
        List<Contradiction> contradictionsList = new();

        string caseId = Guid.NewGuid().ToString();
        string caseDisplayId = GenerateDisplayId.generate(EntityType.CASE);

        // For one profile
        CaseProfile profile = CaseProfiles.Shipment(caseId, false);
        profileDict.TryAdd(profile, truth);
        // precompute
        AddToEvidenceList(truth, profile, evidenceList);
        AddToContradictionList(truth, profile, evidenceList, contradictionsList);
        //

        // For another AND could also create a truth2 to make entirely different from first profile.
        CaseProfile profile2 = CaseProfiles.Shipment(caseId, true);
        profileDict.TryAdd(profile2, truth);
        AddToEvidenceList(truth, profile2, evidenceList);
        AddToContradictionList(truth, profile2, evidenceList, contradictionsList);
        //

        // Render display sentences from templates
        foreach (Evidence evidence in evidenceList)
        {
            evidence.DisplayContent = EvidenceRenderer.Render(evidence);
        }


        return new CaseData
        {
            Id = caseId,
            DisplayId = caseDisplayId,
            //Truth = truth,
            Profiles = profileDict,
            Evidence = evidenceList,
            Contradictions = contradictionsList
        };
    }

    private static void AddToEvidenceList(CaseTruth truth, CaseProfile profile, List<Evidence> evidenceList)
    {
        foreach (EvidenceType type in profile.EvidenceTypes)
        {
            evidenceList.Add(EvidenceFactory.Create(type, truth));
        }
    }

    private static void AddToContradictionList(CaseTruth truth, CaseProfile profile, List<Evidence> evidenceList, List<Contradiction> contradictionsList)
    {
        foreach (FraudType fraud in profile.FraudTypes)
        {
            IFraudInjector injector = FraudInjectorFactory.Create(fraud);

            contradictionsList.Add(injector.Inject(evidenceList, truth, profile.Id));
        }
    }

    // old
    public static List<Evidence> BuildTestAllEvidence()
    {
        // Manually test all evidence. Must provide in truth all properties the evidence uses.
        // 1. Generate the truth
        CaseTruth truth = CaseTruthGenerator.Generate();

        // 2. Generate the evidence pieces
        Evidence invoice =
            EvidenceGenerator
                .CreateInvoice(
                    truth);

        Evidence shippingLog =
            EvidenceGenerator
                .CreateShippingLog(
                    truth);

        Evidence inventory =
            EvidenceGenerator
                .CreateInventoryReport(
                    truth);

        Evidence bankStatement =
            EvidenceGenerator
                .CreateBankStatement(
                    truth);

        Evidence payroll =
            EvidenceGenerator
                .CreatePayrollRecord(
                    truth);

        List<Evidence> evidenceList =
            new()
            {
                invoice,
                shippingLog,
                inventory,
                bankStatement,
                payroll
            };

        //List<Contradiction>
        //    contradictions =
        //        FraudInjector
        //            .InjectShipmentDateFraud(
        //                invoice,
        //                shippingLog);


        foreach (Evidence evidence in evidenceList)
        {
            evidence.DisplayContent =
                EvidenceRenderer.Render(
                    evidence);
        }

        return evidenceList;
    }
}