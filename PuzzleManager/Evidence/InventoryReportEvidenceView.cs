using System;
using TMPro;
using UnityEngine;

public class InventoryReportEvidenceView : EvidenceView
{
    public FactItem saleRecordId;
    public FactItem shipmentQuantity;

    public override void Setup(Evidence evidence)
    {
        base.Setup(evidence);

        foreach (Fact fact in evidence.Facts)
        {
            switch (fact.FactType)
            {
                case FactType.Sale_record_id:
                    saleRecordId.Setup(fact);
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