using System;

// Generate the current truth state.
public static class CaseTruthGenerator
{
    public static CaseTruth Generate()
    {
        return CaseTruthLevels.CaseTruthList[0];
    }
}