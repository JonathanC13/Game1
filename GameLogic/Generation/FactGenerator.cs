using UnityEngine;
using System;
using System.Diagnostics;

// Templates for the Facts, Facts seperated because differenct pieces of Evidence combine different facts. 
public static class FactGenerator
{
    public static Fact CreateVendorFact(
        string evidenceId,
        string vendorName)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.Vendor,

            Values =
            {
                ["VendorName"] = vendorName
            }
        };
    }

    public static Fact CreateAmountDueFact(
        string evidenceId,
        decimal amount)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.AmountDue,

            Values =
            {
                ["Amount"] = amount
            }
        };
    }

    public static Fact CreateShipmentDateFact(
        string evidenceId,
        DateTime date)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.ShipmentDate,

            Values =
            {
                ["Date"] = date
            }
        };
    }

    public static Fact CreateShipmentQuantityFact(
        string evidenceId,
        int quantity)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.ShipmentQuantity,

            Values =
            {
                ["Quantity"] = quantity
            }
        };
    }

    public static Fact CreateShipmentStatusFact(
        string evidenceId,
        ShipmentStatus status)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.ShipmentStatus,

            Values =
            {
                ["Status"] = status.ToString()
            }
        };
    }

    public static Fact CreateEmployeeStatusFact(
        string evidenceId,
        string employeeName,
        EmployeeStatus status)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.EmployeeStatus,

            Values =
            {
                ["EmployeeName"] = employeeName,
                ["Status"] = status.ToString()
            }
        };
    }

    public static Fact CreateInventoryQuantityFact(
        string evidenceId,
        int quantity)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.InventoryQuantity,

            Values =
            {
                ["Quantity"] = quantity
            }
        };
    }

    public static Fact CreatePaymentStatusFact(
        string evidenceId,
        PaymentStatus status)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.PaymentStatus,

            Values =
            {
                ["Status"] = status.ToString()
            }
        };
    }

    public static Fact CreatePaymentAmountFact(
        string evidenceId,
        decimal amount)
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            EvidenceId = evidenceId,

            FactType = FactType.PaymentAmount,

            Values =
            {
                ["Amount"] = amount
            }
        };
    }
}