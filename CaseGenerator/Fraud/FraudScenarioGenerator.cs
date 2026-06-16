using System;
using System.Collections.Generic;
using System.Linq;

// Randomly select fraudCount number of FraudTypes
public static class FraudScenarioGenerator
{
    public static FraudScenario GenerateFromEmployeeFraud(
        DifficultySettings settings)
    {
        List<FraudType> availableFraudTypes =
            new()
            {
                FraudType.EmpPayAmountMismatch,
                FraudType.EmpPaymentStatusMismatch,
                FraudType.EmployeeStatusMismatch
        };

        return new FraudScenario
        {
            // Shuffle
            FraudTypes = availableFraudTypes.OrderBy(x => System.Guid.NewGuid())
                .Take(settings.FraudCount)
                .ToList()
        };
    }

    public static FraudScenario GenerateFromContractorFraud(
        DifficultySettings settings)
    {
        List<FraudType> availableFraudTypes =
            new()
            {
                FraudType.ContractorMismatch,
                FraudType.ContractAmountMismatch,
                FraudType.ContractPaymentStatusMismatch,
        };

        return new FraudScenario
        {
            // Shuffle
            FraudTypes = availableFraudTypes.OrderBy(x => System.Guid.NewGuid())
                .Take(settings.FraudCount)
                .ToList()
        };
    }

    public static FraudScenario GenerateFromSalesFraud(
        DifficultySettings settings)
    {
        List<FraudType> availableFraudTypes =
            new()
            {
                FraudType.BuyerMismatch,
                FraudType.AmountMismatch,
                FraudType.ShipmentDateMismatch,
                FraudType.ShipmentQuantityMismatch,
                FraudType.ShipmentStatusMismatch,
                FraudType.PaymentStatusMismatch
        };

        return new FraudScenario
        {
            // Shuffle
            FraudTypes = availableFraudTypes.OrderBy(x => System.Guid.NewGuid())
                .Take(settings.FraudCount)
                .ToList()
        };
    }

    public static FraudScenario GenerateFromAllFraud(
        DifficultySettings settings)
    {
        FraudType[] availableFraudTypes = (FraudType[])Enum.GetValues(typeof(FraudType));

        return new FraudScenario
        {
            // Shuffle
            FraudTypes = availableFraudTypes.OrderBy(x => System.Guid.NewGuid())
                .Take(settings.FraudCount)
                .ToList()
        };
    }

    public static FraudScenario TestTargetFraud(FraudType fraudType)
    {
        return new FraudScenario
        {
            FraudTypes = new List<FraudType> { fraudType }
        };
    }
}