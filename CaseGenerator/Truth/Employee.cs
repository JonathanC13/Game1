// The class contains the values that describe the Entity during the current Truth state.
using System;
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
        string sb = "Employee \n";
        sb += $"Id: {Id} \n";
        sb += $"EmployeeRecordId: {EmployeeRecordId} \n";
        sb += $"EmployeeId: {EmployeeId} \n";
        sb += $"EmployeePay: {EmployeePay} \n";
        sb += $"EmployeeStatus: {EmployeeStatus} \n";
        sb += $"PaymentStatus: {PaymentStatus} \n";
        Debug.Log(sb);
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