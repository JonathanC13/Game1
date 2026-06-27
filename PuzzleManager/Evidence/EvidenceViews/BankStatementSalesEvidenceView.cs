using System;
using TMPro;
using UnityEngine;

public class BankStatementSalesEvidenceView : EvidenceView
{
    public TMP_Text saleRecordId;
    public FactItem buyer;
    public FactItem amount;
    public FactItem paymentStatus;

    public override void Setup(Evidence evidence)
    {
        base.Setup(evidence);

        foreach (Fact fact in evidence.Facts)
        {
            switch (fact.FactType)
            {
                case FactType.Sale_record_id:
                    saleRecordId.text = FactRenderer.Render(fact);
                    break;

                case FactType.Buyer:
                    buyer.Setup(fact);
                    break;

                case FactType.Amount:
                    amount.Setup(fact);
                    break;

                case FactType.PaymentStatus:
                    paymentStatus.Setup(fact);
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