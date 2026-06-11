// The class contains the values that describe the Entity during the current Truth state.
using System;

public class Employee
{
    public string Id;

    public string Name;

    public EmployeeStatus Status;
}

public static class EmployeeGenerator
{
    public static Employee Generate()
    {
        return new Employee
        {
            Id = Guid.NewGuid().ToString(),
            Name = GenerateDisplayId.generate(EntityType.EMP),
            Status = EnumExtensions.GetRandomValue<EmployeeStatus>()
        };
    }
}