// The definitions for the Fact Types that are associated with different types of evidence

// constants
using System;
using System.Collections.Generic;

public enum FactType
{
    Vendor,
    AmountDue,
    ShipmentStatus,
    ContractSigned,
    EmployeeStatus,
    InventoryCount,
    BankTransfer,
    EmailSender,
    EmailRecipient,
    EmailSubject,
    InventoryStatus,
    PaymentStatus
}
public enum ShippingStatus
{
    Scheduled,
    Occurred,
    Delayed
}

public enum EmployeeStatus
{
    Active,
    Terminated,
    Missing
}

public enum PaymentStatus
{
    Pending,
    Approved,
    Denied,
    Cancelled
}

public enum InventoryStatus
{
    Available,
    Unavailable,
    Reserved,
    Damaged
}

// interface
public abstract class Fact
{
    public string FactId;
    public string EvidenceId;

    public abstract FactType Type { get; }

    public abstract Dictionary<string, object> GetTemplateValues();
}

// concrete
public class AmountDueFact : Fact
{
    public decimal Amount;

    public override FactType Type => FactType.AmountDue;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Amount", Amount }
        };
    }
}

public class VendorFact : Fact
{
    public string VendorId;
    public string VendorName;

    public override FactType Type => FactType.Vendor;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "VendorName", VendorName }
        };
    }
}

public class ShipmentStatusFact : Fact
{
    public string ShipmentId;
    public DateTime Date;
    public ShippingStatus Status;

    public override FactType Type => FactType.ShipmentStatus;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "ShipmentId", ShipmentId },
            { "Date", Date.ToShortDateString() },
            { "Status", Status }
        };
    }
}

public class ContractSigned : Fact
{
    public DateTime Date;

    public override FactType Type => FactType.ContractSigned;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Date", Date.ToShortDateString() }
        };
    }
}

public class EmployeeStatusFact : Fact
{
    public string EmployeeId;
    public string EmployeeName;
    public EmployeeStatus Status;

    public override FactType Type => FactType.EmployeeStatus;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "EmployeeId", EmployeeId },
            { "EmployeeName", EmployeeName },
            { "Status", Status }
        };
    }
}

public class InventoryCountFact : Fact
{
    public string ProductId;
    public int Quantity;

    public override FactType Type => FactType.InventoryCount;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "ProductId", ProductId },
            { "Quantity", Quantity }
        };
    }
}

public class BankTransferFact : Fact
{
    public decimal Amount;
    public string AccountId;

    public override FactType Type => FactType.BankTransfer;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Amount", Amount },
            { "AccountId", AccountId }
        };
    }
}

public class EmailSenderFact : Fact
{
    public string Sender;

    public override FactType Type => FactType.EmailSender;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Sender", Sender }
        };
    }
}

public class EmailRecipientFact : Fact
{
    public string Recipient;

    public override FactType Type => FactType.EmailRecipient;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Recipient", Recipient }
        };
    }
}

public class EmailSubjectFact : Fact
{
    public string Subject;

    public override FactType Type => FactType.EmailSubject;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Subject", Subject }
        };
    }
}


public class InventoryStatusFact : Fact
{
    public InventoryStatus Status;

    public override FactType Type => FactType.InventoryStatus;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Status", Status }
        };
    }
}

public class PaymentStatusFact : Fact
{
    public PaymentStatus Status;

    public override FactType Type => FactType.PaymentStatus;

    public override Dictionary<string, object> GetTemplateValues()
    {
        return new Dictionary<string, object>()
        {
            { "Status", Status }
        };
    }
}

// Templates
public static class FactTemplateRegistry
{
    private static readonly Dictionary<FactType, string>
        Templates =
            new Dictionary<FactType, string>()
            {
                {
                    FactType.Vendor,
                    "Vendor: {VendorName}"
                },

                {
                    FactType.AmountDue,
                    "Amount Due: ${Amount}"
                },

                {
                    FactType.ShipmentStatus,
                    "Shipment dispatched on {Date}, Shipment status: {Status}"
                },

                {
                    FactType.ContractSigned,
                    "Contract signed on {Date}"
                },

                {
                    FactType.EmployeeStatus,
                    "Employee: {EmployeeName}, Status: {Status}"
                },

                {
                    FactType.InventoryCount,
                    "Inventory Count: {Quantity}"
                },

                {
                    FactType.BankTransfer,
                    "Transferred ${Amount} to account {AccountNumber}"
                },

                {
                    FactType.EmailSender,
                    "From: {Sender}"
                },

                {
                    FactType.EmailRecipient,
                    "To: {Recipient}"
                },

                {
                    FactType.EmailSubject,
                    "Subject: {Subject}"
                },

                {
                    FactType.InventoryStatus,
                    "Inventory status: {Status}"
                },

                {
                    FactType.PaymentStatus,
                    "Payment Status: {Status}"
                },
            };

    public static string GetTemplate(FactType factType)
    {
        return Templates[factType];
    }
}

public static class FactRenderer
{
    public static string Render(Fact fact)
    {
        string template = FactTemplateRegistry.GetTemplate(fact.Type);

        var values = fact.GetTemplateValues();

        foreach (var kvp in values)
        {
            template = template.Replace($"{{{kvp.Key}}}", kvp.Value?.ToString());
        }

        return template;
    }
}