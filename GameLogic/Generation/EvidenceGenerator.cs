using System;

public static class EvidenceGenerator
{
    public static Evidence CreateInvoice(CaseTruth truth)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Invoice",
                DisplayId = GenerateDisplayId.generate(EntityType.INV),
                Type = EvidenceType.Invoice
            };

        evidence.Facts.Add(
            FactGenerator.CreateVendorFact(
                evidence.Id,
                truth.Vendor.Name));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence.Id,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence.Id,
                truth.Shipment.Quantity));

        evidence.Facts.Add(
            FactGenerator.CreateAmountDueFact(
                evidence.Id,
                truth.Payment.Amount)
            );

        return evidence;
    }

    public static Evidence CreateShippingLog(CaseTruth truth)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Shipping Log",
                DisplayId = GenerateDisplayId.generate(EntityType.SHIP),
                Type = EvidenceType.ShippingLog
            };

        evidence.Facts.Add(
            FactGenerator.CreateShipmentDateFact(
                evidence.Id,
                truth.Shipment.Date));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentQuantityFact(
                evidence.Id,
                truth.Shipment.Quantity));

        evidence.Facts.Add(
            FactGenerator.CreateShipmentStatusFact(
                evidence.Id,
                truth.Shipment.Status));

        return evidence;
    }

    public static Evidence CreateInventoryReport(CaseTruth truth)
    {
        var evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Inventory Report",
                DisplayId = GenerateDisplayId.generate(EntityType.INVEN),
                Type = EvidenceType.InventoryReport
            };

        evidence.Facts.Add(
            FactGenerator.CreateInventoryQuantityFact(
                evidence.Id,
                truth.Inventory.Quantity)
            );

        return evidence;
    }

    public static Evidence CreateBankStatement(
        CaseTruth truth)
    {
        Evidence evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Bank Statement",
                DisplayId = GenerateDisplayId.generate(EntityType.BANK),
                Type = EvidenceType.BankStatement
            };


        evidence.Facts.Add(
            FactGenerator.CreatePaymentAmountFact(
                evidence.Id,
                truth.Payment.Amount));


        evidence.Facts.Add(
            FactGenerator.CreatePaymentStatusFact(
                evidence.Id,
                truth.Payment.Status));


        return evidence;
    }


    public static Evidence CreatePayrollRecord(
        CaseTruth truth)
    {
        Evidence evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Payroll",
                DisplayId = GenerateDisplayId.generate(EntityType.PAYROLL),
                Type = EvidenceType.PayrollRecord
            };


        evidence.Facts.Add(
            FactGenerator.CreateEmployeeStatusFact(
                evidence.Id,
                truth.Employee.Name,
                truth.Employee.Status));


        return evidence;
    }

    // For email, make variations with different facts so it can contradict different Evidence pieces
    public static Evidence CreateEmail(
        CaseTruth truth)
    {
        Evidence evidence =
            new Evidence
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Email",
                DisplayId = GenerateDisplayId.generate(EntityType.EMAIL),
                Type = EvidenceType.Email
            };

        evidence.Facts.Add(
            FactGenerator.CreateVendorFact(
                evidence.Id,
                truth.Vendor.Name));


        //evidence.Facts.Add(
        //    FactGenerator.CreateShipmentStatusFact(
        //        evidence.Id,
        //        ShipmentStatus.Delivered));


        return evidence;
    }
}