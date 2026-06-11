// Blueprint for CaseTruth. Contains all the Entities that represent the current truth state, the Fact's values are sourced from their associated Entities.
public class CaseTruth
{
    public string TruthId;

    public Vendor Vendor;

    public Shipment Shipment;

    public Employee Employee;

    public Payment Payment;

    public InventoryRecord Inventory;
}