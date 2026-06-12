// Constants for EvidenceType.
public enum EvidenceType
{
    Bank_statement_hr,
    Payroll_Rec,
    HR_rec,

    Email_from_contractor,
    Contract,
    Bank_statement_contract,
    Email_from_bank_contract,

    Email_inv_out,
    Invoice_sale,
    Purchase_order,
    Inventory_report,
    Shipping_log,
    Email_from_shipping,
    Bank_statement_sale,
    Email_from_bank_sale,

    Gambling_ad,
    FastFood_ad,
    MoneyLending_ad,
    FocusPills_ad,
    Cult_ad
}

public enum EvidencePurpose
{
    Required,
    Optional,
    RedHerring,
    Filler
}