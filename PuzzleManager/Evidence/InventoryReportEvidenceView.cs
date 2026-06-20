using System;
using TMPro;
using UnityEngine;

public class InventoryReportEvidenceView : EvidenceView
{
    public TMP_Text saleRecordId;
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