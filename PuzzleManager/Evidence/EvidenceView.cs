using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EvidenceView : MonoBehaviour
{
    public TMP_Text DisplayName;
    public TMP_Text DisplayId;


    public virtual void Setup(Evidence evidence)
    {
        DisplayName.text = evidence.DisplayName;
        DisplayId.text = evidence.DisplayId;
    }
}