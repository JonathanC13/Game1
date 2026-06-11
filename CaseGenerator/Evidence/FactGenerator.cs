using UnityEngine;
using System;
using System.Diagnostics;

// Helper functions to create specific Facts
// Facts like Shipping Date and Shipping Quantity seperated because differenct pieces of Evidence combine different facts and may not need both.
public static class FactGenerator
{
    public static Fact CreateVendorFact(
        Evidence evidence,
        string vendorName)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Vendor,

            Values =
            {
                ["VendorName"] = vendorName
            }
        };
    }

    public static Fact CreateAmountDueFact(
        Evidence evidence,
        decimal amount)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.AmountDue,

            Values =
            {
                ["Amount"] = amount
            }
        };
    }

    public static Fact CreateShipmentDateFact(
        Evidence evidence,
        DateTime date)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ShipmentDate,

            Values =
            {
                ["Date"] = date
            }
        };
    }

    public static Fact CreateShipmentQuantityFact(
        Evidence evidence,
        int quantity)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ShipmentQuantity,

            Values =
            {
                ["Quantity"] = quantity
            }
        };
    }

    public static Fact CreateShipmentStatusFact(
        Evidence evidence,
        ShipmentStatus status)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ShipmentStatus,

            Values =
            {
                ["Status"] = status.ToString()
            }
        };
    }

    public static Fact CreateEmployeeStatusFact(
        Evidence evidence,
        string employeeName,
        EmployeeStatus status)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.EmployeeStatus,

            Values =
            {
                ["EmployeeName"] = employeeName,
                ["Status"] = status.ToString()
            }
        };
    }

    public static Fact CreateInventoryQuantityFact(
        Evidence evidence,
        int quantity)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.InventoryQuantity,

            Values =
            {
                ["Quantity"] = quantity
            }
        };
    }

    public static Fact CreatePaymentStatusFact(
        Evidence evidence,
        PaymentStatus status)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.PaymentStatus,

            Values =
            {
                ["Status"] = status.ToString()
            }
        };
    }

    public static Fact CreatePaymentAmountFact(
        Evidence evidence,
        decimal amount)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.PaymentAmount,

            Values =
            {
                ["Amount"] = amount
            }
        };
    }
}