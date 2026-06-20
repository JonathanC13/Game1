using System;
using TMPro;
using UnityEngine;

public class HRRecordEvidenceView : EvidenceView
{
    public TMP_Text employeeRecordId;
    public TMP_Text employeeId;
    public TMP_Text employeeStatus;

    public override void Setup(Evidence evidence)
    {
        base.Setup(evidence);

        foreach (Fact fact in evidence.Facts)
        {
            switch (fact.FactType)
            {
                case FactType.Employee_record_id:
                    employeeRecordId.text = FactRenderer.Render(fact);
                    break;

                case FactType.EmployeeId:
                    employeeId.text = FactRenderer.Render(fact);
                    break;

                case FactType.EmployeeStatus:
                    employeeStatus.text = FactRenderer.Render(fact);
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