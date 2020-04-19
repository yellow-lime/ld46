using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public GameObject titleScreenGameObj;
    public GameObject creditsScreenGameObj;

    enum SceneType {
        TITLE_SCREEN,
        CREDITS_SCREEN
    }
    private SceneType whatAreWe;

    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.C))
        {
            debugToggleSceneType();
        }
        if (Debug.isDebugBuild && Input.GetKeyUp(KeyCode.Return)) {
            SceneManager.LoadScene("Garden");
        }
        if (!Debug.isDebugBuild && Input.anyKey){
            SceneManager.LoadScene("Garden");
        }
    }

    void debugToggleSceneType(){
        switch(whatAreWe){
            case SceneType.TITLE_SCREEN:
                whatAreWe = SceneType.CREDITS_SCREEN;
                titleScreenGameObj.SetActive(false);
                creditsScreenGameObj.SetActive(true);
                break;
            case SceneType.CREDITS_SCREEN:
                whatAreWe = SceneType.TITLE_SCREEN;
                titleScreenGameObj.SetActive(true);
                creditsScreenGameObj.SetActive(false);
                break;
        }
    }
}
