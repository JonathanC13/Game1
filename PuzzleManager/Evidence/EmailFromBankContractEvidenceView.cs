using System;
using TMPro;
using UnityEngine;

public class EmailFromBankContractEvidenceView : EvidenceView
{
    public TMP_Text contractRecordId;
    public TMP_Text contractor;
    public TMP_Text contractAmount;
    public TMP_Text contractPaymentStatus;

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
                    contractor.text = FactRenderer.Render(fact);
                    break;

                case FactType.ContractAmount:
                    contractAmount.text = FactRenderer.Render(fact);
                    break;

                case FactType.ContractPaymentStatus:
                    contractPaymentStatus.text = FactRenderer.Render(fact);
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