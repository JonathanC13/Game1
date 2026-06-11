using System;

// Helper functions to generate the specific Evidence. 
public static class EvidenceGenerator
{
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
                DisplayId = GenerateDisplayId.generate(EntityType.INV),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        // Picked what value from Truth to assign to fact here so the Evidence can control what goes into the Fact.
        evidence.Facts.Add(
            FactGenerator.CreateVendorFact(
                evidence,
                truth.Vendor.Name));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence,
                truth.Shipment.Quantity));

        evidence.Facts.Add(
            FactGenerator.CreateAmountDueFact(
                evidence,
                truth.Payment.Amount)
            );

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
                DisplayName = "Shipping Log",
                DisplayId = GenerateDisplayId.generate(EntityType.SHIP),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

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
                DisplayName = "Inventory Report",
                DisplayId = GenerateDisplayId.generate(EntityType.INVEN),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        evidence.Facts.Add(
            FactGenerator.CreateInventoryQuantityFact(
                evidence,
                truth.Inventory.Quantity)
            );

        return evidence;
    }

    public static Evidence CreateBankStatement(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        Evidence evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Bank Statement",
                DisplayId = GenerateDisplayId.generate(EntityType.BANK),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };


        evidence.Facts.Add(
            FactGenerator.CreatePaymentAmountFact(
                evidence,
                truth.Payment.Amount));


        evidence.Facts.Add(
            FactGenerator.CreatePaymentStatusFact(
                evidence,
                truth.Payment.Status));


        return evidence;
    }


    public static Evidence CreatePayrollRecord(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        Evidence evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Payroll",
                DisplayId = GenerateDisplayId.generate(EntityType.PAYROLL),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };


        evidence.Facts.Add(
            FactGenerator.CreateEmployeeStatusFact(
                evidence,
                truth.Employee.Name,
                truth.Employee.Status));


        return evidence;
    }

    // For email, make variations with different facts so it can contradict different Evidence pieces
    public static Evidence CreateEmail(
        EvidenceToGenerate e,
        CaseTruth truth,
        string caseId)
    {
        Evidence evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EMAIL),
                Type = e.Type,
                Purpose = e.Purpose,
                CaseId = caseId
            };

        evidence.Facts.Add(
            FactGenerator.CreateVendorFact(
                evidence,
                truth.Vendor.Name));


        //evidence.Facts.Add(
        //    FactGenerator.CreateShipmentStatusFact(
        //        evidence.Id,
        //        ShipmentStatus.Delivered));


        return evidence;
    }
}