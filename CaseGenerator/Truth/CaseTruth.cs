// Blueprint for CaseTruth. Contains all the Entities that represent the current truth state, the Fact's values are sourced from their associated Entities.
using UnityEngine;
public class CaseTruth
{
    public string TruthId;

    public Employee Employee;

    public Contractor Contractor;

    public Contract Contract;

    public PurchaseOrder PurchaseOrder;

    public Shipment Shipment;

    public Payment Payment;

    public void PrintCaseTruth()
    {
        Debug.Log($"CaseTruth\nTruthId: {TruthId}\n");

        Employee.PrintEmployee();
        Contractor.PrintContractor();
        Contract.PrintContract();
        PurchaseOrder.PrintPurchaseOrder();
        Shipment.PrintShipment();
        Payment.PrintPayment();
    }
}