using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroSceneChange : MonoBehaviour
{
    VideoPlayer videoPlayer;
    [SerializeField] int sceneID;

    AsyncOperation sceneLoader;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;

        sceneLoader = SceneManager.LoadSceneAsync(sceneID);
        sceneLoader.allowSceneActivation = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("space")) sceneLoader.allowSceneActivation = true;
    }

    void EndReached(VideoPlayer vp)
    {
        sceneLoader.allowSceneActivation = true;
    }
}
