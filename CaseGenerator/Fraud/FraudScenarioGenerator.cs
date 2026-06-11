using System.Collections.Generic;
using System.Linq;

// Randomly select fraudCount number of FraudTypes
public static class FraudScenarioGenerator
{
    public static FraudScenario Generate(
        DifficultySettings settings)
    {
        List<FraudType> availableFraudTypes =
            new()
            {
                FraudType.AmountMismatch,
                FraudType.VendorMismatch,
                FraudType.ShipmentDateMismatch,
                FraudType.InventoryQuantityMismatch
        };

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