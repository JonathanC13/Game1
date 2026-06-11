using System.Collections.Generic;


public static class FraudTemplates
{
    public static FraudTemplate Get(
        FraudType type)
    {
        switch (type)
        {
            case FraudType.AmountMismatch:
                return AmountMismatch();

            case FraudType.VendorMismatch:
                return VendorMismatch();

            case FraudType.ShipmentDateMismatch:
                return ShipmentDateMismatch();

            case FraudType.InventoryQuantityMismatch:
                return InventoryQuantityMismatch();

            default:
                throw new System.Exception("Fraud template missing");
        }
    }

    private static FraudTemplate AmountMismatch()
    {
        return new FraudTemplate
        {
            Type =  FraudType.AmountMismatch,

            TargetFacts =
            {
                FactType.AmountDue
            },

            RequiredEvidence =
            {
                EvidenceType.Invoice,

                EvidenceType.BankStatement
            },

            OptionalEvidence =
            {
                EvidenceType.Email
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate VendorMismatch()
    {
        return new FraudTemplate
        {
            Type = FraudType.VendorMismatch,

            TargetFacts =
            {
                FactType.Vendor
            },


            RequiredEvidence =
            {
                EvidenceType.Invoice,

                EvidenceType.Email
            },


            OptionalEvidence =
            {
                EvidenceType.BankStatement
            },


            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate ShipmentDateMismatch()
    {
        return new FraudTemplate
        {
            Type = FraudType.ShipmentDateMismatch,

            TargetFacts =
            {
                FactType.ShipmentDate
            },

            RequiredEvidence =
            {
                EvidenceType.Invoice,

                EvidenceType.ShippingLog
            },

            OptionalEvidence =
            {
                EvidenceType.Email
            },

            ContradictionsCreated = 1
        };
    }

    private static FraudTemplate InventoryQuantityMismatch()
    {
        return new FraudTemplate
        {
            Type = FraudType.InventoryQuantityMismatch,

            TargetFacts =
            {
                FactType.InventoryQuantity
            },

            RequiredEvidence =
            {
                EvidenceType.Invoice,

                EvidenceType.InventoryReport
            },

            OptionalEvidence =
            {
                EvidenceType.Email
            },

            ContradictionsCreated = 1
        };
    }

}