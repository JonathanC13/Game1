using System;

// Switch board
public static class EvidenceFactory
{
    public static Evidence Create(
        EvidenceToGenerate e, 
        CaseTruth truth,
        string caseId)
    {
        switch (e.Type)
        {
            case EvidenceType.Invoice:
                return EvidenceGenerator.CreateInvoice(e, truth, caseId);

            case EvidenceType.ShippingLog:
                return EvidenceGenerator.CreateShippingLog(e, truth, caseId);

            case EvidenceType.InventoryReport:
                return EvidenceGenerator.CreateInventoryReport(e, truth, caseId);

            case EvidenceType.BankStatement:
                return EvidenceGenerator.CreateBankStatement(e, truth, caseId);

            case EvidenceType.PayrollRecord:
                return EvidenceGenerator.CreatePayrollRecord(e, truth, caseId);

            case EvidenceType.Email:
                return EvidenceGenerator.CreateEmail(e, truth, caseId);

            default:
                throw new Exception($"No generator for {e.Type}");
        }
    }
}