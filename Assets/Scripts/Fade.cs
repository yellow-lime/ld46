using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void FadeOut(){
        StartCoroutine (DoFadeOut());
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
