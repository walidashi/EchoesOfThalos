using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;
    Image fadeImage; // a ui that alpha = 0 at first and it helps in fading the image

    void Awake()
    {
        instance = this;
        fadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeOut(float duration) //functions used in other scripts in cutscene
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t / duration)); //black it out
            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration) 
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t / duration)); //revert the fade
            yield return null;
        }
    }
}
