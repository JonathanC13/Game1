using System;
using TMPro;
using UnityEngine;

public class PurchaseOrderEvidenceView : EvidenceView
{
    public TMP_Text saleRecordId;
    public FactItem buyer;
    public FactItem amount;
    public FactItem shipmentQuantity;

    public override void Setup(Evidence evidence)
    {
        base.Setup(evidence);

        // Setup Facts
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

                case FactType.ShipmentQuantity:
                    shipmentQuantity.Setup(fact);
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