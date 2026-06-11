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
            case EvidenceType.Bank_statement_hr:
                return EvidenceGenerator.CreateBankStatementHR(e, truth, caseId);

            case EvidenceType.Payroll_Rec:
                return EvidenceGenerator.CreatePayrollRecord(e, truth, caseId);

            case EvidenceType.HR_rec:
                return EvidenceGenerator.CreateHRRecord(e, truth, caseId);

            default:
                throw new Exception($"No generator for {e.Type}");
        }
    }
}