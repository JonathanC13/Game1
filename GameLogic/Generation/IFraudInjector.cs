using System.Collections.Generic;

public interface IFraudInjector
{
    Contradiction Inject(List<Evidence> evidence, CaseTruth truth, string caseId);
}