using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] int gameSceneId;
    [SerializeField] GameObject screen;
    [SerializeField] GameObject credits;
    AsyncOperation sceneChanger;


    private void Start()
    {
        sceneChanger = SceneManager.LoadSceneAsync(gameSceneId);
        sceneChanger.allowSceneActivation = false;

        LeanTween.scale(screen, new Vector3(1, 1, 1), 0.75f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);
    }

    public void OpenCredits(bool state)
    {
        if(state == true)
        {
            LeanTween.scale(credits, new Vector3(1, 1, 1), 0.1f);
        }
        else
        {
            LeanTween.scale(credits, new Vector3(0,0,0), 0.1f);
        }
    }

    public void StartGame()
    {
        LeanTween.scale(screen, new Vector3(0, 0, 0), 0.3f);
        Invoke(nameof(ChangeScene), 0.4f);
    }

    void ChangeScene()
    {
        sceneChanger.allowSceneActivation = true;

    }

    public void Quit()
    {
        Application.Quit();
    }

}
