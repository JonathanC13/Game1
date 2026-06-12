using System.Collections.Generic;

// Ensure the FactType is only used once across all chosen FraudTypes in the scenario.
public static class FraudScenarioValidator
{
    public static bool Validate(
        FraudScenario scenario)
    {
        HashSet<FactType> usedFacts = new();

        foreach (var fraud in scenario.FraudTypes)
        {
            FraudTemplate template = FraudTemplates.Get(fraud);

            foreach (var fact in template.TargetFacts)
            {
                if (usedFacts.Contains(fact))
                {
                    return false;
                }

                usedFacts.Add(fact);
            }
        }

        return true;
    }

}