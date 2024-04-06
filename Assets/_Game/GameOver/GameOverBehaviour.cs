using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] int MenuScene;
    [SerializeField] int GameScene;
    [SerializeField] TMP_Text genCounter;
    [SerializeField] TMP_Text timer;
    [SerializeField] Transform materialHolder;
    // Start is called before the first frame update
    void OnEnable()
    {
        genCounter.text = $"Generation Reached: {stats.Generation}";
        int minutes = (int)stats.gameTime / 60;
        int seconds = (int)stats.gameTime % 60;
        timer.text = $"Time Played: {minutes}:{seconds}";

        materialHolder.Find("Water").GetComponent<TMP_Text>().text = $"Water: {stats.totalWater}";
        materialHolder.Find("Wood").GetComponent<TMP_Text>().text = $"Wood: {stats.totalWood}";
        materialHolder.Find("Metal").GetComponent<TMP_Text>().text = $"Metal: {stats.totalMetal}";
        materialHolder.Find("Oil").GetComponent<TMP_Text>().text = $"Oil: {stats.totalOil}";
        materialHolder.Find("Energy").GetComponent<TMP_Text>().text = $"Energy: {stats.totalEnergy}";


        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.2f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(MenuScene);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void Menu()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.2f);
        Invoke(nameof(ChangeScene), 0.3f);
    }

    public void Retry()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.2f);
        Invoke(nameof(ReloadScene), 0.3f);
    }

}
