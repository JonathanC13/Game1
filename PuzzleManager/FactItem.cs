using TMPro;
using UnityEngine;

public class FactItem : MonoBehaviour
{
    public TMP_Text FactText;

    public Fact FactObj;

    public LinkableItem LinkItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Fact fact)
    {
        FactObj = fact;
        FactText.text = FactRenderer.Render(fact);

        LinkItem.Setup(fact.Id);
    }
}
