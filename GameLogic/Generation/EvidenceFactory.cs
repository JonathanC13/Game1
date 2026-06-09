using System;

public static class EvidenceFactory
{
    public static Evidence Create(EvidenceType type, CaseTruth truth)
    {
        switch (type)
        {
            case EvidenceType.Invoice:

                return EvidenceGenerator
                    .CreateInvoice(truth);


            case EvidenceType.ShippingLog:

                return EvidenceGenerator
                    .CreateShippingLog(truth);


            case EvidenceType.InventoryReport:

                return EvidenceGenerator
                    .CreateInventoryReport(truth);


            case EvidenceType.BankStatement:

                return EvidenceGenerator
                    .CreateBankStatement(truth);


            case EvidenceType.PayrollRecord:

                return EvidenceGenerator
                    .CreatePayrollRecord(truth);


            case EvidenceType.Email:

                return EvidenceGenerator
                    .CreateEmail(truth);


            default:

                throw new Exception(
                    $"No generator for {type}");
        }
    }
}