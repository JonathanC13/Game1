// The class contains the values that describe the Entity during the current Truth state.
using System;

public class InventoryRecord
{
    public string Id;
    public string ProductId;

    public int Quantity;
}

public static class InventoryRecordGenerator
{
    public static InventoryRecord Generate()
    {
        return new InventoryRecord
        {
            Id = Guid.NewGuid().ToString(),
            ProductId = GenerateDisplayId.generate(EntityType.PROD),
            Quantity = UnityEngine.Random.Range(100, 10000)
        };
    }
}