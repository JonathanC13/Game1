using System;
using TMPro;
using UnityEngine;

public class PayrollRecordEvidenceView : EvidenceView
{
    public TMP_Text employeeRecordId;
    public TMP_Text employeeId;
    public TMP_Text employeeStatus;
    public TMP_Text employeePayAmount;
    public TMP_Text employeePaymentStatus;

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

                case FactType.EmpPayAmount:
                    employeePayAmount.text = FactRenderer.Render(fact);
                    break;

                case FactType.EmpPaymentStatus:
                    employeePaymentStatus.text = FactRenderer.Render(fact);
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