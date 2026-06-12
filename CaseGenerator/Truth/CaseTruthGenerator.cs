public static class CaseTruthGenerator
{
    public static CaseTruth Generate()
    {
        PurchaseOrder purchaseOrder = PurchaseOrderGenerator.Generate();

        return new CaseTruth
        {
            Employee = EmployeeGenerator.Generate(),

            Contractor = ContractorGenerator.Generate(),

            Contract = ContractGenerator.Generate(),

            PurchaseOrder = purchaseOrder,

            Shipment = ShipmentGenerator.Generate(purchaseOrder),

            Payment = PaymentGenerator.Generate(purchaseOrder)
        };
    }
}