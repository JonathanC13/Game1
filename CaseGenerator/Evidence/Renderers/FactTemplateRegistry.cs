using System.Collections.Generic;

// Templates for Fact's property to sentences.
public static class FactTemplateRegistry
{
    public static readonly Dictionary<FactType, string> Templates =
        new()
        {
            {
                FactType.Employee_record_id,
                "Employee record id: {EmployeeRecordId}"
            },
            {
                FactType.EmployeeId,
                "Employee id: {EmployeeId}"
            },
            {
                FactType.EmpPayAmount,
                "Employee payment amount: {EmpPayAmount}"
            },
            {
                FactType.EmpPaymentStatus,
                "Employee payment status: {EmpPaymentStatus}"
            },
            {
                FactType.EmployeeStatus,
                "Employee status: {EmployeeStatus}"
            },
            {
                FactType.Contract_record_id,
                "Contract record id: {ContractRecordId}"
            },
            {
                FactType.Contractor,
                "Contractor: {Contractor}"
            },
            {
                FactType.ContractAmount,
                "Contract amount: {ContractAmount}"
            },
            {
                FactType.ContractPaymentStatus,
                "Contract payment status: {ContractPaymentStatus}"
            },
            {
                FactType.Sale_record_id,
                "Sale record id: {SaleRecordId}"
            },
            {
                FactType.Buyer,
                "Buyer: {Buyer}"
            },
            {
                FactType.Amount,
                "Buy amount: {Amount}"
            },
            {
                FactType.ShipmentDate,
                "Shipment date: {ShipmentDate}"
            },
            {
                FactType.ShipmentQuantity,
                "Shipment quantity: {ShipmentQuantity}"
            },
            {
                FactType.ShipmentStatus,
                "Shipment status: {ShipmentStatus}"
            },
            {
                FactType.PaymentStatus,
                "Payment status: {PaymentStatus}"
            }
        };
}