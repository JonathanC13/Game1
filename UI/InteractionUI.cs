using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public GameObject promptObject;

    public TextMeshProUGUI actionText;
    public TextMeshProUGUI keyText;


    void Start()
    {
        keyText.text = "Press E";
        Hide();
    }

    public void Show(string action)
    {
        promptObject.SetActive(true);
        actionText.text = action;
    }

    public void Hide()
    {
        promptObject.SetActive(false);
    }
}