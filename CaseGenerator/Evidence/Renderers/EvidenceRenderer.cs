using System.Text;

// For the Evidence, genearate all the associated Facts' values into sentences.
public static class EvidenceRenderer
{
    public static string Render(Evidence evidence)
    {
        var sb = new StringBuilder();

        foreach (var fact in evidence.Facts)
        {
            sb.AppendLine(FactRenderer.Render(fact));
        }

        return sb.ToString();
    }
}