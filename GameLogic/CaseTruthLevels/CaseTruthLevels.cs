using System;
using System.Collections.Generic;

public static class CaseTruthLevels
{
    public static List<CaseTruth> CaseTruthList { get; } = new();

    // test all current evidence by providing all Entities' properties a value. Add to when adding more Entities
    private static CaseTruth allTruth =
        new CaseTruth
        {
            Vendor = new Vendor
            {
                Id = "V001",
                Name = "ABC Supplies"
            },

            Shipment = new Shipment
            {
                Id = "S001",
                Date = DateTime.Today,
                Quantity = 500,
                Status = ShipmentStatus.Scheduled
            },

            Payment = new Payment
            {
                Id = "P001",
                Amount = 12500m,
                Status = PaymentStatus.Approved
            },

            Employee = new Employee
            {
                Id = "E001",
                Name = "John Smith",
                Status = EmployeeStatus.Active
            },

            Inventory = new InventoryRecord
            {
                ProductId = "Widget",
                Quantity = 500
            }
        };

    static CaseTruthLevels()
    {
        CaseTruthList.Add(allTruth);
    }
};