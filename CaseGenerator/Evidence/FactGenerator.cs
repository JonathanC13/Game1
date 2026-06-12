using UnityEngine;
using System;
using System.Diagnostics;

// Helper functions to create specific Facts
// Facts like Shipping Date and Shipping Quantity seperated because differenct pieces of Evidence combine different facts and may not need both.
public static class FactGenerator
{
    public static Fact CreateEmployeeRecordIdFact(
        Evidence evidence,
        string employeeRecordId
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Employee_record_id,

            Values =
            {
                ["EmployeeRecordId"] = employeeRecordId
            }
        };
    }

    public static Fact CreateContractRecordIdFact(
        Evidence evidence,
        string contractRecordId
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Contract_record_id,

            Values =
            {
                ["ContractRecordId"] = contractRecordId
            }
        };
    }

    public static Fact CreateSaleRecordIdFact(
        Evidence evidence,
        string saleRecordId
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Sale_record_id,

            Values =
            {
                ["SaleRecordId"] = saleRecordId
            }
        };
    }

    public static Fact CreateEmployeeIdFact(
        Evidence evidence,
        string employeeId
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.EmployeeId,

            Values =
            {
                ["EmployeeId"] = employeeId
            }
        };
    }

    public static Fact CreateEmpPayAmountFact(
        Evidence evidence,
        string empPayAmount
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.EmpPayAmount,

            Values =
            {
                ["EmpPayAmount"] =empPayAmount
            }
        };
    }

    public static Fact CreateEmpPaymentStatusFact(
        Evidence evidence,
        PaymentStatus empPaymentStatus
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.EmpPaymentStatus,

            Values =
            {
                ["EmpPaymentStatus"] = empPaymentStatus
            }
        };
    }

    public static Fact CreateEmployeeStatusFact(
        Evidence evidence,
        EmployeeStatus employeeStatus
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.EmployeeStatus,

            Values =
            {
                ["EmployeeStatus"] = employeeStatus
            }
        };
    }

    public static Fact CreateContractorFact(
        Evidence evidence,
        string contractor
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Contractor,

            Values =
            {
                ["Contractor"] = contractor
            }
        };
    }

    public static Fact CreateContractAmountFact(
        Evidence evidence,
        string contractAmount
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ContractAmount,

            Values =
            {
                ["ContractAmount"] = contractAmount
            }
        };
    }

    public static Fact CreateContractPaymentStatusFact(
        Evidence evidence,
        PaymentStatus paymentStatus
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ContractPaymentStatus,

            Values =
            {
                ["ContractPaymentStatus"] = paymentStatus
            }
        };
    }

    public static Fact CreateBuyerFact(
        Evidence evidence,
        string buyer
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Buyer,

            Values =
            {
                ["Buyer"] = buyer
            }
        };
    }

    public static Fact CreateAmountFact(
        Evidence evidence,
        string amount
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.Amount,

            Values =
            {
                ["Amout"] = amount
            }
        };
    }

    public static Fact CreateShipmentDateFact(
        Evidence evidence,
        DateTime date
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ShipmnetDate,

            Values =
            {
                ["ShipmentDate"] = date
            }
        };
    }

    public static Fact CreateShipmentQuantityFact(
        Evidence evidence,
        int quantity
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ShipmentQuantity,

            Values =
            {
                ["ShipmentQuantity"] = quantity
            }
        };
    }

    public static Fact CreateShipmentStatusFact(
        Evidence evidence,
        ShipmentStatus status
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.ShipmentStatus,

            Values =
            {
                ["ShipmentStatus"] = status
            }
        };
    }

    public static Fact CreatePaymentStatusFact(
        Evidence evidence,
        PaymentStatus status
        )
    {
        return new Fact
        {
            Id = Guid.NewGuid().ToString(),

            Evidence = evidence,

            FactType = FactType.PaymentStatus,

            Values =
            {
                ["PaymentStatus"] = status
            }
        };
    }
}