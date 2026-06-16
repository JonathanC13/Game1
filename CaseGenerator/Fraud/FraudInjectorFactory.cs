using System;
using System.Collections.Generic;

// switch board for the FraudType to injector.
public static class FraudInjectorFactory
{

    public static IFraudInjector Create(
        FraudType type)
    {

        switch (type)
        {
            case FraudType.EmpPayAmountMismatch:
                return new EmpPayAmountMismatchInjector();

            case FraudType.EmpPaymentStatusMismatch:
                return new EmpPaymentStatusMismatchInjector();

            case FraudType.EmployeeStatusMismatch:
                return new EmployeeStatusMismatchInjector();

            case FraudType.ContractorMismatch:
                return new ContractorMismatchInjector();

            case FraudType.ContractAmountMismatch:
                return new ContractAmountMismatchInjector();

            case FraudType.ContractPaymentStatusMismatch:
                return new ContractPaymentStatusMismatchInjector();

            case FraudType.BuyerMismatch:
                return new BuyerMismatchInjector();

            case FraudType.AmountMismatch:
                return new AmountMismatchInjector();

            case FraudType.ShipmentDateMismatch:
                return new ShipmentDateMismatchInjector();

            case FraudType.ShipmentQuantityMismatch:
                return new ShipmentQuantityMismatchInjector();

            case FraudType.ShipmentStatusMismatch:
                return new ShipmentStatusMismatchInjector();

            case FraudType.PaymentStatusMismatch:
                return new PaymentStatusMismatchInjector();

            default:
                throw new Exception("No injector");
        }
    }
}