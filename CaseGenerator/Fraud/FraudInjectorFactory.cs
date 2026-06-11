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
            case FraudType.ShipmentDateMismatch:
                return new ShipmentDateMismatchInjector();

            default:
                throw new Exception("No injector");
        }
    }
}