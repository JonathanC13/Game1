using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public static class FraudTemplates
{
    public static FraudTemplate Get(
        FraudType type)
    {
        switch (type)
        {
            case FraudType.EmpPayAmountMismatch:
                return EmpPayAmountMismatch(type);

            case FraudType.EmpPaymentStatusMismatch:
                return EmpPaymentStatusMismatch(type);

            case FraudType.EmployeeStatusMismatch:
                return EmployeeStatusMismatch(type);

            case FraudType.ContractorMismatch:
                return ContractorMismatch(type);

            case FraudType.ContractAmountMismatch:
                return ContractAmountMismatch(type);

            case FraudType.ContractPaymentStatusMismatch:
                return ContractPaymentStatusMismatch(type);

            case FraudType.BuyerMismatch:
                return BuyerMismatch(type);

            case FraudType.AmountMismatch:
                return AmountMismatch(type);

            case FraudType.ShipmentDateMismatch:
                return ShipmnetDateMismatch(type);

            case FraudType.ShipmentQuantityMismatch:
                return ShipmentQuantityMismatch(type);

            case FraudType.ShipmentStatusMismatch:
                return ShipmentStatusMismatch(type);

            case FraudType.PaymentStatusMismatch:
                return PaymentStatusMismatch(type);

            default:
                throw new System.Exception("Fraud template missing");
        }
    }

    private static List<EvidenceType> GetPairForFactType(FactType ft)
    {
        var random = new System.Random();
        List<EvidenceType> evidenceType = EvidenceTypeFactTypeList.FE_LIST[ft];

        if (evidenceType.Count < 2)
        {
            throw new System.Exception($"For FactType {ft}, not enough EvidenceTypes");
        }

        int firstIndex = random.Next(evidenceType.Count);

        int secondIndex;
        do
        {
            secondIndex = random.Next(evidenceType.Count);
        }
        while (secondIndex == firstIndex);

        return new List<EvidenceType> { 
            evidenceType[firstIndex], 
            evidenceType[secondIndex] 
        };
    }

    private static FraudTemplate EmpPayAmountMismatch(FraudType fraudType)
    {
        FactType ft = FactType.EmpPayAmount;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            // For specific Fraud, add optional pieces.
            OptionalEvidence =
            {
                EvidenceType.Bank_statement_hr,
                EvidenceType.Payroll_Rec,
                EvidenceType.HR_rec
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate EmpPaymentStatusMismatch(FraudType fraudType)
    {
        FactType ft = FactType.EmpPaymentStatus;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Bank_statement_hr,
                EvidenceType.Payroll_Rec,
                EvidenceType.HR_rec
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate EmployeeStatusMismatch(FraudType fraudType)
    {
        FactType ft = FactType.EmployeeStatus;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Bank_statement_hr,
                EvidenceType.Payroll_Rec,
                EvidenceType.HR_rec
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ContractorMismatch(FraudType fraudType)
    {
        FactType ft = FactType.Contractor;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Email_from_bank_contract,
                EvidenceType.Contract,
                EvidenceType.Bank_statement_contract,
                EvidenceType.Email_from_bank_contract,
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ContractAmountMismatch(FraudType fraudType)
    {
        FactType ft = FactType.ContractAmount;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Email_from_bank_contract,
                EvidenceType.Contract,
                EvidenceType.Bank_statement_contract,
                EvidenceType.Email_from_bank_contract,
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ContractPaymentStatusMismatch(FraudType fraudType)
    {
        FactType ft = FactType.ContractPaymentStatus;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Email_from_bank_contract,
                EvidenceType.Contract,
                EvidenceType.Bank_statement_contract,
                EvidenceType.Email_from_bank_contract,
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate BuyerMismatch(FraudType fraudType)
    {
        FactType ft = FactType.Buyer;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Purchase_order,
                EvidenceType.Email_inv_out,
                EvidenceType.Invoice_sale,
                EvidenceType.Shipping_log,
                EvidenceType.Email_from_shipping,
                EvidenceType.Inventory_report,
                EvidenceType.Bank_statement_sale,
                EvidenceType.Email_from_bank_sale
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate AmountMismatch(FraudType fraudType)
    {
        FactType ft = FactType.Amount;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Purchase_order,
                EvidenceType.Email_inv_out,
                EvidenceType.Invoice_sale,
                EvidenceType.Shipping_log,
                EvidenceType.Email_from_shipping,
                EvidenceType.Inventory_report,
                EvidenceType.Bank_statement_sale,
                EvidenceType.Email_from_bank_sale
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ShipmnetDateMismatch(FraudType fraudType)
    {
        FactType ft = FactType.ShipmentDate;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Purchase_order,
                EvidenceType.Email_inv_out,
                EvidenceType.Invoice_sale,
                EvidenceType.Shipping_log,
                EvidenceType.Email_from_shipping,
                EvidenceType.Inventory_report,
                EvidenceType.Bank_statement_sale,
                EvidenceType.Email_from_bank_sale
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ShipmentQuantityMismatch(FraudType fraudType)
    {
        FactType ft = FactType.ShipmentQuantity;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Purchase_order,
                EvidenceType.Email_inv_out,
                EvidenceType.Invoice_sale,
                EvidenceType.Shipping_log,
                EvidenceType.Email_from_shipping,
                EvidenceType.Inventory_report,
                EvidenceType.Bank_statement_sale,
                EvidenceType.Email_from_bank_sale
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ShipmentStatusMismatch(FraudType fraudType)
    {
        FactType ft = FactType.ShipmentStatus;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Purchase_order,
                EvidenceType.Email_inv_out,
                EvidenceType.Invoice_sale,
                EvidenceType.Shipping_log,
                EvidenceType.Email_from_shipping,
                EvidenceType.Inventory_report,
                EvidenceType.Bank_statement_sale,
                EvidenceType.Email_from_bank_sale
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate PaymentStatusMismatch(FraudType fraudType)
    {
        FactType ft = FactType.PaymentStatus;

        return new FraudTemplate
        {
            Type = fraudType,

            TargetFacts =
            {
                ft
            },

            RequiredEvidence = GetPairForFactType(ft),

            OptionalEvidence =
            {
                EvidenceType.Purchase_order,
                EvidenceType.Email_inv_out,
                EvidenceType.Invoice_sale,
                EvidenceType.Shipping_log,
                EvidenceType.Email_from_shipping,
                EvidenceType.Inventory_report,
                EvidenceType.Bank_statement_sale,
                EvidenceType.Email_from_bank_sale
            },

            ContradictionsCreated = 1
        };
    }
}