using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue")]
public class DialogueAsset : ScriptableObject
{
    public string Speaker;

    [TextArea]
    public string Text;

    public DialogueOption[] Options;
}
