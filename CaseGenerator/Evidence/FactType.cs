// Constants for FactType.
public enum FactType
{
    Employee_record_id,
    EmployeeId,
    EmpPayAmount,
    EmpPaymentStatus,
    EmployeeStatus,

    Contract_record_id,
    Contractor,
    ContractAmount,
    ContractPaymentStatus,

    Sale_record_id,
    Buyer,
    Amount,
    ShipmnetDate,
    ShipmentQuantity,
    ShipmentStatus,
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
