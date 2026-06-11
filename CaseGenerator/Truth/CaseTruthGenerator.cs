public static class CaseTruthGenerator
{
    public static CaseTruth Generate()
    {
        return new CaseTruth
        {
            Vendor = VendorGenerator.Generate(),

            Shipment = ShipmentGenerator.Generate(),

            Payment = PaymentGenerator.Generate(),

            Inventory = InventoryRecordGenerator.Generate(),

            Employee = EmployeeGenerator.Generate()
        };
    }
}