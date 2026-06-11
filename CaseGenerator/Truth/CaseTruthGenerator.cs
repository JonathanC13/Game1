public static class CaseTruthGenerator
{
    public static CaseTruth Generate()
    {
        return new CaseTruth
        {
            Employee = EmployeeGenerator.Generate(),

            Contractor = ContractorGenerator.Generate(),

            Contract = ContractGenerator.Generate(),

            PurchaseOrder = PurchaseOrder.Generate(),

            Shipment = ShipmentGenerator.Generate(PurchaseOrder),

            Payment = PaymentGenerator.Generate(PurchaseOrder)
        };
    }
}