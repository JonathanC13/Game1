using System;
using System.Collections.Generic;

public static class GenerateRandom
{
    public static decimal Money(
        decimal min, 
        decimal max)
    {
        Random random = new Random();
        decimal randomMoney = min + (decimal)(random.NextDouble() * (double)(max - min));
        return Math.Round(randomMoney, 2);
    }
}