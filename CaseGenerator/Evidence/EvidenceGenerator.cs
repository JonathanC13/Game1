using System;

// Helper functions to generate the specific Evidence. 
public static class EvidenceGenerator
{
    public static Evidence CreateBankStatementHR(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Bank Statement",
                DisplayId = GenerateDisplayId.generate(EntityType.BNK_HR),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Picked what value from Truth to assign to fact here so the Evidence can control what goes into the Fact.
        evidence.Facts.Add(
            FactGenerator.CreateEmployeeIdFact(
                evidence,
                truth.Employee.EmployeeId));

        evidence.Facts.Add(
            FactGenerator.CreateEmpPayAmountFact(
                evidence,
                truth.Employee.EmployeePay));

        evidence.Facts.Add(
            FactGenerator.CreateEmpPaymentStatusFact(
                evidence,
                truth.Employee.PaymentStatus));

        return evidence;
    }

    public static Evidence CreatePayrollRecord(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Payroll Record",
                DisplayId = GenerateDisplayId.generate(EntityType.PAYREC),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateEmployeeIdFact(
                evidence,
                truth.Employee.EmployeeId));

        evidence.Facts.Add(
            FactGenerator.CreateEmployeeStatusFact(
                evidence,
                truth.Employee.EmployeeStatus));

        evidence.Facts.Add(
            FactGenerator.CreateEmpPayAmountFact(
                evidence,
                truth.Employee.EmployeePay));

        evidence.Facts.Add(
            FactGenerator.CreateEmpPaymentStatusFact(
                evidence,
                truth.Employee.PaymentStatus));

        return evidence;
    }

    public static Evidence CreateHRRecord(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "HR Record",
                DisplayId = GenerateDisplayId.generate(EntityType.HRREC),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateEmployeeIdFact(
                evidence,
                truth.Employee.EmployeeId));

        evidence.Facts.Add(
            FactGenerator.CreateEmployeeStatusFact(
                evidence,
                truth.Employee.EmployeeStatus));

        return evidence;
    }
    public static Evidence CreateInvoice2(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Invoice",
                DisplayId = GenerateDisplayId.generate(EntityType.INV_SAL),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateEmpPaymentStatusFact(
                evidence,
                truth.Employee.PaymentStatus));

        return evidence;
    }
}