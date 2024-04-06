using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public void Pause(bool state)
    {
        if (state) Time.timeScale = 1f;
        else Time.timeScale = 0f;
    }

    public void ChangeScene(string SceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }
}
