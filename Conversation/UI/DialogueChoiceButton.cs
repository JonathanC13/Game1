using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

// goes on prefab
public class DialogueChoiceButton : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] Button button;

    public Button Button => button;

    public event Action Clicked;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            Clicked?.Invoke();
        });
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void Bind(string text, Action onClick)
    {
        gameObject.SetActive(true);
        label.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }

    public void Show(
        string text)
    {
        gameObject.SetActive(true);

        label.text = text;

        button.onClick.RemoveAllListeners();
    }

    public void Hide()
    {
        button.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}