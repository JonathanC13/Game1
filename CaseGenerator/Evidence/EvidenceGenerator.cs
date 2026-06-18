using System;
using System.Collections.Generic;
using UnityEngine;

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

        //Picked what value from Truth to assign to fact here so the Evidence can control what goes into the Fact.
        evidence.Facts.Add(
            FactGenerator.CreateEmployeeRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.EMP_REC) : truth.Employee.EmployeeRecordId));

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

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

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
            FactGenerator.CreateEmployeeRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.EMP_REC) : truth.Employee.EmployeeRecordId));

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

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

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
            FactGenerator.CreateEmployeeRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.EMP_REC) : truth.Employee.EmployeeRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateEmployeeIdFact(
                evidence,
                truth.Employee.EmployeeId));

        evidence.Facts.Add(
            FactGenerator.CreateEmployeeStatusFact(
                evidence,
                truth.Employee.EmployeeStatus));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateEmailFromContractor(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EML_CNT),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateContractRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.CON_REC) : truth.Contract.ContractRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateContractorFact(
                evidence,
                truth.Contractor.ContractorId));

        evidence.Facts.Add(
            FactGenerator.CreateContractAmountFact(
                evidence,
                truth.Contract.ContractAmount));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateContract(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Contract",
                DisplayId = GenerateDisplayId.generate(EntityType.CNTRCT),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateContractRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.CON_REC) : truth.Contract.ContractRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateContractorFact(
                evidence,
                truth.Contractor.ContractorId));

        evidence.Facts.Add(
            FactGenerator.CreateContractAmountFact(
                evidence,
                truth.Contract.ContractAmount));

        evidence.Facts.Add(
            FactGenerator.CreatePaymentStatusFact(
                evidence,
                truth.Contract.PaymentStatus));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateBankStatementContract(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Bank statement",
                DisplayId = GenerateDisplayId.generate(EntityType.BNK_CNT),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateContractRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.CON_REC) : truth.Contract.ContractRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateContractorFact(
                evidence,
                truth.Contractor.ContractorId));

        evidence.Facts.Add(
            FactGenerator.CreateContractAmountFact(
                evidence,
                truth.Contract.ContractAmount));

        evidence.Facts.Add(
            FactGenerator.CreateContractPaymentStatusFact(
                evidence,
                truth.Contract.PaymentStatus));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateEmailFromBankContract(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EML_BNK_C),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateContractRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.CON_REC) : truth.Contract.ContractRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateContractorFact(
                evidence,
                truth.Contractor.ContractorId));

        evidence.Facts.Add(
            FactGenerator.CreateContractAmountFact(
                evidence,
                truth.Contract.ContractAmount));

        evidence.Facts.Add(
            FactGenerator.CreateContractPaymentStatusFact(
                evidence,
                truth.Contract.PaymentStatus));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateEmailInvoiceOutgoing(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EML_INV),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

    // Facts
    evidence.Facts.Add(
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateBuyerFact(
                evidence,
                truth.PurchaseOrder.BuyerId));

        evidence.Facts.Add(
            FactGenerator.CreateAmountFact(
                evidence,
                truth.PurchaseOrder.Amount));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence,
                truth.PurchaseOrder.Quantity));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateInvoice(
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
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateBuyerFact(
                evidence,
                truth.PurchaseOrder.BuyerId));

        evidence.Facts.Add(
            FactGenerator.CreateAmountFact(
                evidence,
                truth.PurchaseOrder.Amount));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence,
                truth.Shipment.Quantity));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateBankStatementSales(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Bank statement",
                DisplayId = GenerateDisplayId.generate(EntityType.BNK_SAL),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateBuyerFact(
                evidence,
                truth.PurchaseOrder.BuyerId));

        evidence.Facts.Add(
            FactGenerator.CreateAmountFact(
                evidence,
                truth.PurchaseOrder.Amount));

        evidence.Facts.Add(
            FactGenerator.CreatePaymentStatusFact(
                evidence,
                truth.Payment.Status));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateEmailFromBankSale(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EML_BNK_SAL),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateBuyerFact(
                evidence,
                truth.PurchaseOrder.BuyerId));

        evidence.Facts.Add(
            FactGenerator.CreateAmountFact(
                evidence,
                truth.PurchaseOrder.Amount));

        evidence.Facts.Add(
            FactGenerator.CreatePaymentStatusFact(
                evidence,
                truth.Payment.Status));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateShippingLog(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Shipping log",
                DisplayId = GenerateDisplayId.generate(EntityType.SHPLOG),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence,
                truth.Shipment.Quantity));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentStatusFact(
                evidence,
                truth.Shipment.Status));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateEmailFromShipping(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EML_SHP),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence,
                truth.Shipment.Quantity));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentStatusFact(
                evidence,
                truth.Shipment.Status));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    public static Evidence CreateInventoryReport(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Inventory report",
                DisplayId = GenerateDisplayId.generate(EntityType.INVENT),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts
        evidence.Facts.Add(
            FactGenerator.CreateSaleRecordIdFact(
                evidence,
                e.Purpose == EvidencePurpose.RedHerring ? GenerateDisplayId.generate(EntityType.SALE_REC) : truth.PurchaseOrder.SaleRecordId));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence,
                truth.Shipment.Quantity));

        EvidenceTypeFactTypeList.ValidateFactsAdded(evidence);

        return evidence;
    }

    // Red Herrings, don't need Facts. Just template and paper asset.
    public static Evidence CreateGamblingAd(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Adverstisment",
                DisplayId = GenerateDisplayId.generate(EntityType.AD),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts

        return evidence;
    }

    public static Evidence CreateFastFoodAd(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Coupons",
                DisplayId = GenerateDisplayId.generate(EntityType.AD),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts

        return evidence;
    }

    public static Evidence CreateMoneyLendingAd(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Borrow money",
                DisplayId = GenerateDisplayId.generate(EntityType.AD),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts

        return evidence;
    }

    public static Evidence CreateFocusPillsAd(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Focus pills",
                DisplayId = GenerateDisplayId.generate(EntityType.AD),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts

        return evidence;
    }

    public static Evidence CreateCultAd(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Join us",
                DisplayId = GenerateDisplayId.generate(EntityType.AD),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Facts

        return evidence;
    }
}