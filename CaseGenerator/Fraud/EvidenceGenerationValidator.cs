using System.Collections.Generic;
using System.Linq;

public static class EvidenceGenerationValidator
{
    public static bool Validate(
        List<Evidence> evidence,
        List<Contradiction> contradictions)
    {
        foreach (var contradiction in contradictions)
        {
            Evidence a = evidence.FirstOrDefault(x => x.Id == contradiction.EvidenceAId);

            Evidence b = evidence.FirstOrDefault(x =>x.Id == contradiction.EvidenceBId);

            if (a == null ||
               b == null)
            {
                return false;
            }

            if (a.Id == b.Id)
            {
                return false;
            }


            bool factAExists =
                a.Facts.Any(
                    x =>
                    x.Type ==
                    contradiction.FactA);



            bool factBExists =
                b.Facts.Any(
                    x =>
                    x.Type ==
                    contradiction.FactB);



            if (!factAExists ||
               !factBExists)
            {
                return false;
            }

        }


        return true;
    }

}