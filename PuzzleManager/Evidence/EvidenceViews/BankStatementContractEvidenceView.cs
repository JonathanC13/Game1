using System;
using TMPro;
using UnityEngine;

public class BankStatementContractEvidenceView : EvidenceView
{
    public TMP_Text contractRecordId;
    public FactItem contractor;
    public FactItem contractAmount;
    public FactItem contractPaymentStatus;

    public override void Setup(Evidence evidence)
    {
        base.Setup(evidence);

        foreach (Fact fact in evidence.Facts)
        {
            switch (fact.FactType)
            {
                case FactType.Contract_record_id:
                    contractRecordId.text = FactRenderer.Render(fact);
                    break;

                case FactType.Contractor:
                    contractor.Setup(fact);
                    break;

                case FactType.ContractAmount:
                    contractAmount.Setup(fact);
                    break;

                case FactType.ContractPaymentStatus:
                    contractPaymentStatus.Setup(fact);
                    break;

                default:
                    Debug.LogWarning(
                        $"No TMP_Text found for {fact.FactType}"
                    );
                    break;
            }
        }

    }
}