using System;
using TMPro;
using UnityEngine;

public class BankStatementHREvidenceView : EvidenceView
{
    public FactItem employeeRecordId;
    public FactItem employeeId;
    public FactItem employeePayAmount;
    public FactItem employeePaymentStatus;

    public override void Setup(Evidence evidence)
    {
        base.Setup(evidence);

        foreach (Fact fact in evidence.Facts)
        {
            switch(fact.FactType)
            {
                case FactType.Employee_record_id:
                    employeeRecordId.Setup(fact);
                    break;

                case FactType.EmployeeId:
                    employeeId.Setup(fact);
                    break;

                case FactType.EmpPayAmount:
                    employeePayAmount.Setup(fact);
                    break;

                case FactType.EmpPaymentStatus:
                    employeePaymentStatus.Setup(fact);
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