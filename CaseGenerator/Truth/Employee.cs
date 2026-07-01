// The class contains the values that describe the Entity during the current Truth state.
using System;
using System.Text;
using UnityEngine;

public class Employee
{
    public string Id;

    public string EmployeeRecordId;

    public string EmployeeId;

    public string EmployeePay;

    public EmployeeStatus EmployeeStatus;

    public PaymentStatus PaymentStatus;

    public void PrintEmployee()
    {
        StringBuilder sb = new StringBuilder("Employee");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"EmployeeRecordId: {EmployeeRecordId}");
        sb.AppendLine($"EmployeeId: {EmployeeId}");
        sb.AppendLine($"EmployeePay: {EmployeePay}");
        sb.AppendLine($"EmployeeStatus: {EmployeeStatus}");
        sb.AppendLine($"PaymentStatus: {PaymentStatus}");
        Debug.Log(sb.ToString());
    }
}

public static class EmployeeGenerator
{
    public static Employee Generate()
    {
        return new Employee
        {
            Id = Guid.NewGuid().ToString(),
            EmployeeRecordId = GenerateDisplayId.generate(EntityType.EMP_REC),
            EmployeeId = GenerateDisplayId.generate(EntityType.EMP),
            EmployeePay = GenerateRandom.Money(1000, 10000).ToString(),
            EmployeeStatus = EnumExtensions.GetRandomValue<EmployeeStatus>(),
            PaymentStatus = EnumExtensions.GetRandomValue<PaymentStatus>()
        };
    }
}