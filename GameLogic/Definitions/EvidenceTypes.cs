// Evidence Types

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public enum EvidenceType
{
    Invoice,
    ShippingLog,
    Contract,
    Email,
    PayrollRecord,
    InventoryReport,
    BankStatement
}

public class Evidence
{
    public string Id;
    public EvidenceType Type;
    public List<Fact> Facts = new List<Fact>();

    public string DisplayContent;
}

public class EvidenceArchetype
{
    public EvidenceType Type;
    public FactType[] RequiredFacts;
    public FactType[] OptionalFacts;
}

public static class EvidenceRegistry
{
    public static readonly Dictionary<EvidenceType, EvidenceArchetype> Archetypes = new();

    static EvidenceRegistry()
    {
        // add evidence archetypes
        EvidenceRegistry.Archetypes[EvidenceType.Invoice] =
            new EvidenceArchetype
            {
                Type = EvidenceType.Invoice,

                RequiredFacts = new[]
                {
                    FactType.Vendor,
                    FactType.AmountDue
                },

                OptionalFacts = new[]
                {
                    FactType.ShipmentStatus
                }
            };

        EvidenceRegistry.Archetypes[EvidenceType.ShippingLog] =
            new EvidenceArchetype
            {
                Type = EvidenceType.ShippingLog,

                RequiredFacts = new[]
                {
                    FactType.ShipmentStatus
                },

                OptionalFacts = Array.Empty<FactType>()
            };

        EvidenceRegistry.Archetypes[EvidenceType.Contract] =
            new EvidenceArchetype
            {
                Type = EvidenceType.Contract,

                RequiredFacts = new[]
                {
                    FactType.Vendor,
                    FactType.ContractSigned
                },

                OptionalFacts = Array.Empty<FactType>()
            };

        EvidenceRegistry.Archetypes[EvidenceType.Email] =
        new EvidenceArchetype
        {
            Type = EvidenceType.Email,

            RequiredFacts = new[]
            {
                FactType.EmailSender,
                FactType.EmailRecipient,
                FactType.EmailSubject
            },

            OptionalFacts = new[]
            {
                FactType.InventoryStatus,
                FactType.ShipmentStatus,
                FactType.PaymentStatus,
                FactType.ContractSigned,
                FactType.EmployeeStatus,
                FactType.Vendor
            }
        };

        EvidenceRegistry.Archetypes[EvidenceType.PayrollRecord] =
            new EvidenceArchetype
            {
                Type = EvidenceType.PayrollRecord,

                RequiredFacts = new[]
                {
                    FactType.EmployeeStatus,
                    FactType.AmountDue
                },

                OptionalFacts = Array.Empty<FactType>()
            };

        EvidenceRegistry.Archetypes[EvidenceType.InventoryReport] =
            new EvidenceArchetype
            {
                Type = EvidenceType.InventoryReport,

                RequiredFacts = new[]
                {
                    FactType.InventoryCount
                },

                OptionalFacts = Array.Empty<FactType>()
            };

        EvidenceRegistry.Archetypes[EvidenceType.BankStatement] =
            new EvidenceArchetype
            {
                Type = EvidenceType.BankStatement,

                RequiredFacts = new[]
                {
                    FactType.BankTransfer
                },

                OptionalFacts = Array.Empty<FactType>()
            };
    }
}

public static class EvidenceRenderer
{
    public static string Render(Evidence evidence)
    {
        var sb = new System.Text.StringBuilder();

        foreach (var fact in evidence.Facts)
        {
            sb.AppendLine(FactRenderer.Render(fact));
        }

        return sb.ToString();
    }
}