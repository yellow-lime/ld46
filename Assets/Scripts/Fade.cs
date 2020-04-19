using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // public CanvasGroup canvasGroup;
    public Image image;
    public float colorLerpDuration = 3f;

    public void FadeTo(Image image, Color targetColor){
        StartCoroutine (DoFadeTo(image, targetColor));
    }

    IEnumerator DoFadeTo(Image image, Color targetColor)
    {
        image.color = Color.Lerp(image.color, targetColor, colorLerpDuration);
        yield return null;
    }

    // all code below is unused >:/

    IEnumerator DoFadeToOld(Color targetColor){
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        yield return null;
    }

    public void FadeIn(){
        // StartCoroutine (DoFadeOut);
    }

    IEnumerator DoFadeOut() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        while(canvasGroup.alpha > 0){
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        yield return null;
    }
    
}
