// Constants for FactType.
public enum FactType
{
    Vendor,
    AmountDue,

    ShipmentDate,
    ShipmentQuantity,
    ShipmentStatus,

    EmployeeStatus,

    InventoryQuantity,

    PaymentStatus,
    PaymentAmount
}

public enum ShipmentStatus
{
    Scheduled,
    InTransit,
    Delivered,
    Delayed,
    Cancelled
}

public enum EmployeeStatus
{
    Active,
    Terminated,
    Missing
}

public enum PaymentStatus
{
    Pending,
    Approved,
    Denied,
    Cancelled
}
