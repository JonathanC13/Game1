public static class FactPairKey
{
    // create normalized order of the Key for fast lookup
    public static string Create(
        string factAId,
        string factBId)
    {

        if (string.Compare(factAId, factBId) < 0)
        {
            return factAId + "|" + factBId;
        }

        return factBId + "|"+ factAId;
    }

}