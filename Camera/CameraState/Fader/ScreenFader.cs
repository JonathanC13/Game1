using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    // coroutine
    public IEnumerator FadeOut(float duration)
    {
        yield return Fade(0f, 1f, duration);
    }

    public IEnumerator FadeIn(float duration)
    {
        yield return Fade(1f, 0f, duration);
    }

    private IEnumerator Fade(float start, float end, float duration)
    {
        float t = 0;

        // set the alpha during the fade.
        while (t < duration)
        {
            t += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(start, end, t / duration);

            yield return null;
        }

        // snap to end
        canvasGroup.alpha = end;
    }


}
