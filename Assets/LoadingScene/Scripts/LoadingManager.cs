using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] TMP_Text progressText;
    [SerializeField] float timeBeforeLoading = 1f;
    public static int nextSceneIndex { get; private set; } = -1;
    public static string nextSceneName { get; private set; } = string.Empty;


    public static void ChangeNextScene(int sceneIndex)
    {
        nextSceneIndex = sceneIndex;
    }

    public static void ChangeNextScene(string sceneName)
    {
        nextSceneName = sceneName;
    }


    public IEnumerator LoadNextScene()
    {

        if (string.IsNullOrEmpty(nextSceneName) && nextSceneIndex == -1)
        { Debug.LogWarning("Next scene not chosen. Loading aborted"); SceneManager.LoadScene(0); yield break; }

        yield return new WaitForSeconds(timeBeforeLoading);

        AsyncOperation loadSceneAsync;
        loadSceneAsync = string.IsNullOrEmpty(nextSceneName) ? SceneManager.LoadSceneAsync(nextSceneName) : SceneManager.LoadSceneAsync(nextSceneIndex);
        loadSceneAsync.allowSceneActivation = false;

        while (loadSceneAsync.progress < 0.9f)
        {
            progressText.text = $"( {loadSceneAsync.progress * 100} / 100%)";
            yield return new WaitForSeconds(0.1f);
        }
        loadSceneAsync.allowSceneActivation = true;

        nextSceneIndex = -1;
        nextSceneName = string.Empty;
    }

    private void Start()
    {
        StartCoroutine(LoadNextScene());
    }
}
