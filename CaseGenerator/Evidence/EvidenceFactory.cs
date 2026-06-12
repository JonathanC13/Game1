using System;

// Switch board
public static class EvidenceFactory
{
    public static Evidence Create(
        EvidenceToGenerate e, 
        CaseTruth truth,
        string caseId)
    {
        switch (e.Type)
        {
            case EvidenceType.Bank_statement_hr:
                return EvidenceGenerator.CreateBankStatementHR(e, truth, caseId);

            case EvidenceType.Payroll_Rec:
                return EvidenceGenerator.CreatePayrollRecord(e, truth, caseId);

            case EvidenceType.HR_rec:
                return EvidenceGenerator.CreateHRRecord(e, truth, caseId);

            case EvidenceType.Email_from_contractor:
                return EvidenceGenerator.CreateEmailFromContractor(e, truth, caseId);

            case EvidenceType.Contract:
                return EvidenceGenerator.CreateContract(e, truth, caseId);

            case EvidenceType.Bank_statement_contract:
                return EvidenceGenerator.CreateBankStatementContract(e, truth, caseId);

            case EvidenceType.Email_from_bank_contract:
                return EvidenceGenerator.CreateEmailFromBankContract(e, truth, caseId);

            case EvidenceType.Email_inv_out:
                return EvidenceGenerator.CreateEmailInvoiceOutgoing(e, truth, caseId);

            case EvidenceType.Invoice_sale:
                return EvidenceGenerator.CreateInvoice(e, truth, caseId);

            case EvidenceType.Bank_statement_sale:
                return EvidenceGenerator.CreateBankStatementSales(e, truth, caseId);

            case EvidenceType.Email_from_bank_sale:
                return EvidenceGenerator.CreateEmailFromBankSale(e, truth, caseId);

            case EvidenceType.Shipping_log:
                return EvidenceGenerator.CreateShippingLog(e, truth, caseId);

            case EvidenceType.Email_from_shipping:
                return EvidenceGenerator.CreateEmailFromShipping(e, truth, caseId);

            case EvidenceType.Inventory_report:
                return EvidenceGenerator.CreateInventoryReport(e, truth, caseId);

            case EvidenceType.Gambling_ad:
                return EvidenceGenerator.CreateGamblingAd(e, truth, caseId);

            case EvidenceType.FastFood_ad:
                return EvidenceGenerator.CreateFastFoodAd(e, truth, caseId);

            case EvidenceType.MoneyLending_ad:
                return EvidenceGenerator.CreateMoneyLendingAd(e, truth, caseId);

            case EvidenceType.FocusPills_ad:
                return EvidenceGenerator.CreateFocusPillsAd(e, truth, caseId);

            case EvidenceType.Cult_ad:
                return EvidenceGenerator.CreateCultAd(e, truth, caseId);
    

            default:
                throw new Exception($"No generator for {e.Type}");
        }
    }
}