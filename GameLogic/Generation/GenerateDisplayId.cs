using System;

public static class GenerateDisplayId
{
    public static string generate(EntityType identifier)
    {
        int number = UnityEngine.Random.Range(1000, 9999);

        return $"{identifier.ToString()}-{DateTime.Now.Year}-{number}";
    }
}