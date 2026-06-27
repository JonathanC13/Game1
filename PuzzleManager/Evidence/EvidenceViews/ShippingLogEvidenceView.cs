using System;
using TMPro;
using UnityEngine;

public class ShippingLogEvidenceView : EvidenceView
{
    public TMP_Text saleRecordId;
    public FactItem shipmentDate;
    public FactItem shipmentQuantity;
    public FactItem shipmentStatus;

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

                case FactType.ShipmentDate:
                    shipmentDate.Setup(fact);
                    break;

                case FactType.ShipmentQuantity:
                    shipmentQuantity.Setup(fact);
                    break;

                case FactType.ShipmentStatus:
                    shipmentStatus.Setup(fact);
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