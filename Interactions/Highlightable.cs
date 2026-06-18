
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private GameObject outlineObject;
    [SerializeField] private Material outlineMaterial;

    [Header("Settings")]
    [SerializeField] private float baseWidth = 0.02f;
    [SerializeField] private float pulseAmount = 0.01f;
    [SerializeField] private float fadeSpeed = 8f;
    [SerializeField] private float pulseSpeed = 2f;

    float currentWidth;
    float targetActive; // 0 = off, 1 = on
    bool isFocused;

    void Start()
    {
        // IMPORTANT: avoid shared material issues
        outlineMaterial = new Material(outlineMaterial);
        outlineObject.GetComponent<Renderer>().material = outlineMaterial;

        currentWidth = 0f;
        SetOutline(0f);
        outlineObject.SetActive(false);
    }

    void Update()
    {
        if (!isFocused)
        {
            return;
        }

        // pulse target width
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        float targetWidth = baseWidth + pulse;

        // smooth movement
        currentWidth = Mathf.Lerp(currentWidth, targetWidth, Time.deltaTime * fadeSpeed);

        SetOutline(currentWidth);
    }

    public void Highlight(bool state)
    {
        isFocused = state;

        if (state)
        {
            outlineObject.SetActive(true);
        }
        else
        {
            // when leaving, fade out smoothly
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    System.Collections.IEnumerator FadeOut()
    {
        float start = currentWidth;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;

            currentWidth = Mathf.Lerp(start, 0f, t);
            SetOutline(currentWidth);

            yield return null;
        }

        currentWidth = 0f;
        outlineObject.SetActive(false);
    }

    private void SetOutline(float value)
    {
        outlineMaterial.SetFloat("_OutlineWidth", value);
    }
}