// For the Fact, generate all values into sentences from the FactTemplateRegistry.Templates
public static class FactRenderer
{
    public static string Render(Fact fact)
    {
        string template = FactTemplateRegistry.Templates[fact.FactType];

        foreach (var value in fact.Values)
        {
            template =
                template.Replace(
                    $"{{{value.Key}}}",
                    value.Value.ToString());
        }

        return template;
    }
}