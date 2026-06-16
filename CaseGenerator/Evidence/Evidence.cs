using System.Collections.Generic;
using UnityEngine;

// Blueprint for Evidence. Contains all the information for an Evidence piece.
public class Evidence
{
    public string Id;
    public string DisplayName;
    public string DisplayId;

    public EvidenceType Type;
    public EvidencePurpose Purpose;

    public string CaseId;

    // All associated Facts to this Evidence
    public List<Fact> Facts = new();
    
    // The exact string that will be displayed to the user, built from Facts into templates.
    public string DisplayContent;

    public void PrintEvidence()
    {
        string sb = "Evidence \n";
        sb += $"Id: {Id} \n";
        sb += $"DisplayName: {DisplayName} \n";
        sb += $"DisplayId: {DisplayId} \n";
        sb += $"Type: {Type} \n";
        sb += $"Purpose: {Purpose} \n";
        sb += $"CaseId: {CaseId} \n";
        sb += $"DisplayContent: {DisplayContent} \n";
        sb += "Facts: \n";

        foreach (Fact fact in Facts)
        {
            sb += fact.GetFactInfo();
        }

        Debug.Log(sb);
    }
}

public class EvidenceToGenerate
{
    public EvidenceType Type;

    public EvidencePurpose Purpose;
}

public static class EvidenceTypeFactTypeList
{
    public static Dictionary<EvidenceType, List<FactType>> EF_LIST = new Dictionary<EvidenceType, List<FactType>>
        {
            [EvidenceType.Bank_statement_hr] = new List<FactType> { FactType.EmployeeId, FactType.EmpPayAmount, FactType.EmpPaymentStatus },
            [EvidenceType.Payroll_Rec] = new List<FactType> { FactType.EmployeeId, FactType.EmployeeStatus, FactType.EmpPayAmount, FactType.EmpPaymentStatus },
            [EvidenceType.HR_rec] = new List<FactType> { FactType.EmployeeId, FactType.EmployeeStatus },
            [EvidenceType.Email_from_contractor] = new List<FactType> { FactType.Contractor, FactType.ContractAmount },
            [EvidenceType.Contract] = new List<FactType> { FactType.Contractor, FactType.ContractAmount, FactType.ContractPaymentStatus },
            [EvidenceType.Bank_statement_contract] = new List<FactType> { FactType.Contractor, FactType.ContractAmount, FactType.ContractPaymentStatus },
            [EvidenceType.Email_from_bank_contract] = new List<FactType> { FactType.Contractor, FactType.ContractAmount, FactType.ContractPaymentStatus },
            [EvidenceType.Email_inv_out] = new List<FactType> { FactType.Buyer, FactType.Amount, FactType.ShipmentDate, FactType.ShipmentQuantity },
            [EvidenceType.Invoice_sale] = new List<FactType> { FactType.Buyer, FactType.Amount, FactType.ShipmentDate, FactType.ShipmentQuantity },
            [EvidenceType.Purchase_order] = new List<FactType> { FactType.Buyer, FactType.Amount, FactType.ShipmentQuantity },
            [EvidenceType.Inventory_report] = new List<FactType> { FactType.ShipmentQuantity },
            [EvidenceType.Shipping_log] = new List<FactType> { FactType.ShipmentDate, FactType.ShipmentQuantity, FactType.ShipmentStatus },
            [EvidenceType.Email_from_shipping] = new List<FactType> { FactType.ShipmentDate, FactType.ShipmentQuantity, FactType.ShipmentStatus },
            [EvidenceType.Bank_statement_sale] = new List<FactType> { FactType.Buyer, FactType.Amount, FactType.PaymentStatus },
            [EvidenceType.Email_from_bank_sale] = new List<FactType> { FactType.Buyer, FactType.Amount, FactType.PaymentStatus }
        };

    public static Dictionary<FactType, List<EvidenceType>> FE_LIST = new();

    static EvidenceTypeFactTypeList()
    {
        foreach (var kvp in EF_LIST)
        {
            foreach (var value in kvp.Value)
            {
                if (!FE_LIST.TryGetValue(value, out var list))
                {
                    list = new List<EvidenceType>();
                    FE_LIST[value] = list;
                }

                list.Add(kvp.Key);
            }
        }
    }
    
    public static bool ValidateFactsAdded(Evidence evidence)
    {
        HashSet<FactType> need = new HashSet<FactType>(EF_LIST[evidence.Type]);
        HashSet<FactType> have = new();

        foreach (Fact f in evidence.Facts)
        {
            if (need.Contains(f.FactType) && !have.Contains(f.FactType))
            {
                have.Add(f.FactType);
            } else
            {
                return false;
            }
        }

        if (need.Count != have.Count)
        {
            throw new System.Exception($"{evidence.Type}: Evidence missing facts; Have: {string.Join(", ", have)}, Need: {string.Join(", ", need)}");
            //return false;
        }

        return need.Count == have.Count;
    }

}

    
    