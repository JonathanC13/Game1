using TMPro;
using UnityEngine;

public class FactItem : MonoBehaviour
{
    public TMP_Text FactText;

    public Fact FactObj;

    public LinkableItem LinkItem;

    public string Descriptor;

    public FactItem() { }

    public void Setup(Fact fact)
    {
        FactObj = fact;
        FactText.text = FactRenderer.Render(fact);
        Descriptor = FactRenderer.Render(fact);
        LinkItem.Setup(fact.Id);
    }

    public void Partial(Fact fact)
    {
        FactObj = fact;
        Descriptor = FactRenderer.Render(fact);
    }
}
