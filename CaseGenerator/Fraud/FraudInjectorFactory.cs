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

            //case FraudType.ContractAmountMismatch:
            //    return new ContractAmountMismatch();

            //case FraudType.ContractPaymentStatusMismatch:
            //    return new ContractPaymentStatusMismatch();

            //case FraudType.BuyerMismatch:
            //    return new BuyerMismatch();

            //case FraudType.AmountMismatch:
            //    return new AmountMismatch();

            case FraudType.ShipmnetDateMismatch:
                return new ShipmentDateMismatchInjector();

            //case FraudType.ShipmentQuantityMismatch:
            //    return new ShipmentQuantityMismatch();

            //case FraudType.ShipmentStatusMismatch:
            //    return new ShipmentStatusMismatch();

            //case FraudType.PaymentStatusMismatch:
            //    return new PaymentStatusMismatch();

            default:
                throw new Exception("No injector");
        }
    }
}