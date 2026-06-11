// Constants for FactType.
public enum FactType
{
    EmployeeId,
    EmpPayAmount,
    EmpPaymentStatus,
    EmployeeStatus,

    Contractor,
    ContractAmount,
    ContractPaymentStatus,

    Buyer,
    Amount,
    ShipmnetDate,
    ShipmentQuantity,
    PaymentStatus
}

public enum ShipmentStatus
{
    Scheduled,
    InTransit,
    Delivered,
    Lost,
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
