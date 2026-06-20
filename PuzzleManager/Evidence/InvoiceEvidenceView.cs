using System;
using TMPro;
using UnityEngine;

public class InvoiceEvidenceView : EvidenceView
{
    public TMP_Text saleRecordId;
    public TMP_Text buyer;
    public TMP_Text amount;
    public TMP_Text shipmentDate;
    public TMP_Text shipmentQuantity;

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
                    buyer.text = FactRenderer.Render(fact);
                    break;

                case FactType.Amount:
                    amount.text = FactRenderer.Render(fact);
                    break;

                case FactType.ShipmentDate:
                    shipmentDate.text = FactRenderer.Render(fact);
                    break;

                case FactType.ShipmentQuantity:
                    shipmentQuantity.text = FactRenderer.Render(fact);
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