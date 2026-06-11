// Constants for EvidenceType.
public enum EvidenceType
{
    Invoice,
    ShippingLog,
    PayrollRecord,
    InventoryReport,
    BankStatement,
    Email
}

public enum EvidencePurpose
{
    Required,
    Optional,
    RedHerring,
    Filler
}