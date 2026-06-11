using System;
using System.Collections.Generic;

public static class EnumExtensions
{
    public static T GetRandomValue<T>() where T : Enum
    {
        var values = (T[])Enum.GetValues(typeof(T));
        return values[new Random().Next(values.Length)];
    }
}