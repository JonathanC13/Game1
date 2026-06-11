using System;
using System.Collections.Generic;
using System.Linq;

public class CaseBuilder
{
    public CaseData Build(
        DifficultyLevel difficulty)
    {
        DifficultySettings settings = DifficultyProfiles.Get(difficulty);

        int caseAttempts = 0;
        // try to generate valid Case
        while (caseAttempts < 100)
        {
            caseAttempts += 1;
            // Generate the scenario with the number of Frauds
            FraudScenario fraudScenario;
            int attempts = 0;
            // Validate the FraudScenario is unambiguous in contradictions before proceeding generating the case.
            do
            {
                fraudScenario = FraudScenarioGenerator.Generate(settings);
                attempts += 1;

                if (attempts > 100)
                {
                    // just try to start, Easy should always be valid since only choosing 1 FraudType which means no conflict.
                    settings = DifficultyProfiles.Get(DifficultyLevel.Easy);
                }
                else if (attempts > 200)
                {
                    // hard stop
                    throw new System.Exception("Unable to create valid fraud plan");
                }
            }
            while (!FraudScenarioValidator.Validate(fraudScenario));

            // Generate Case's Truth
            string caseId = Guid.NewGuid().ToString();
            string displayId = GenerateDisplayId.generate(EntityType.CASE);
            CaseTruth truth = new CaseTruth();
            List<EvidenceType> evidenceTypes = new();
            List<EvidenceToGenerate> evidenceToGenerate = new();
            List<Evidence> evidence = new();
            List<Fact> factsInvolved = new();
            List<Contradiction> contradictions = new();

            // For the chosen FraudTypes to consolidate the requirements.
            foreach (var fraud in fraudScenario.FraudTypes)
            {
                FraudTemplate template = FraudTemplates.Get(fraud);
                // OK to modify Lists through parameters. Building the collections.
                EvidenceSelector.Select(evidenceTypes, evidenceToGenerate, template, settings.OptionalEvidenceChance);
            }

            // Add Red Herring evidence
            RedHerringGenerator.Add(evidenceTypes, evidenceToGenerate, settings);

            // If after not enough documents, add enough documents for difficulty
            EvidenceCountBalancer.EnsureMinimum(evidenceTypes, evidenceToGenerate, settings);

            // Generate the Evidence (Evidence generates its facts.)
            foreach (EvidenceToGenerate e in evidenceToGenerate)
            {
                evidence.Add(EvidenceFactory.Create(e, truth, caseId));
            }

            // inject fraud based on fraud types
            foreach (var fraud in fraudScenario.FraudTypes)
            {
                IFraudInjector injector = FraudInjectorFactory.Create(fraud);   // Get the object.

                contradictions.Add(injector.Inject(evidence, truth, caseId, factsInvolved));   // Inject
            }

            // Check if all Facts for contradictions created.
            if (!EvidenceFactValidator.Validate(evidence, factsInvolved, contradictions))
            {
                continue;
            }

            // Render display sentences from templates
            foreach (Evidence e in evidence)
            {
                e.DisplayContent = EvidenceRenderer.Render(e);
            }

            // Shuffle the evidence pieces
            evidence = evidence.OrderBy(x => System.Guid.NewGuid())
                .ToList();

            // Build the contradiction index
            ContradictionIndex index = new ContradictionIndex();
            index.Build(contradictions);

            return new CaseData
            {
                Id = caseId,
                DisplayId = displayId,

                Difficulty = settings,

                Evidence = evidence,

                Contradictions = contradictions,

                ContradictionIndex = index
            };
        }

        throw new System.Exception("Unable to create valid fraud plan");
    }

    private void GenerateEvidence(List<Evidence> evidence, CaseTruth truth, string caseId, EvidencePurpose purpose)
    {

    }

}