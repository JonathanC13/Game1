using System;
using System.Collections.Generic;
using Unity.VisualScripting;

// CaseBuilder chooses a Case profile then creates the evidence needed for it.
public static class CaseProfiles
{
    public static CaseProfile Shipment(string caseId, bool Fraud = false)
    {
        string caseProfileId = Guid.NewGuid().ToString();

        CaseProfile profile = new CaseProfile
        {
            Id = caseProfileId,
            DisplayId = GenerateDisplayId.generate(EntityType.PROF),
            Name = Fraud == true ? "Shipment Fraud" : "Shipment",
            CaseId = caseId,

            EvidenceTypes =
            {
                EvidenceType.Invoice,

                EvidenceType.ShippingLog,

                EvidenceType.Email,

                EvidenceType.InventoryReport
            },

            FraudTypes = { },

            ContradictionCount = 0
        };

        if (Fraud == true)
        {
            profile.Name = "Shipment Fraud";
            profile.FraudTypes.Add(FraudType.ShipmentDateMismatch);
            profile.ContradictionCount = 1;
        }

        return profile;

    }


    public static CaseProfile PaymentFraud()
    {
        return new CaseProfile
        {
            Name = "Payment Fraud",

            EvidenceTypes =
            {
                EvidenceType.Invoice,

                EvidenceType.BankStatement,

                EvidenceType.Email
            }
        };
    }


    public static CaseProfile EmployeeFraud()
    {
        return new CaseProfile
        {
            Name = "Ghost Employee",

            EvidenceTypes =
            {
                EvidenceType.PayrollRecord,

                EvidenceType.BankStatement,

                EvidenceType.Email
            }
        };
    }
}