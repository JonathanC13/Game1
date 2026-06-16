// For the Fact, generate all values into sentences from the FactTemplateRegistry.Templates
using System;

public static class FactRenderer
{
    public static string Render(Fact fact)
    {
        string template = FactTemplateRegistry.Templates[fact.FactType];

        foreach (var value in fact.Values)
        {
            if (value.Value is DateTime dateValue)
            {
                template = template.Replace($"{{{value.Key}}}", dateValue.ToString("yyyy-MM-dd"));
            }
            else
            {
                template = template.Replace($"{{{value.Key}}}", value.Value.ToString());
            }
        }

        return template;
    }
}