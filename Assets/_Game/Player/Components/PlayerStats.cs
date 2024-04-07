using System;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers.Sounds;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    [Header("Game Settings")] public float GameTickTime = 1f;
    public float GenerationTimer = 120f;
    float genTime = 0f;
    public bool upgradeOpen = false;
    [SerializeField] private TMP_Text[] statsText;
    public float gameTime { get; private set; } = 0f;

    public float totalWater { get; private set; } = 0f;
    public float totalWood { get; set; } = 0f;
    public float totalMetal { get; private set; } = 0f;
    public float totalEnergy { get; private set; } = 0f;
    public float totalOil { get; private set; } = 0f;

    public float efficiency = .75f;


    [Header("Materials")] [Header("Water")]
    public float Water;

    [ReadOnly] public float WaterGain = 1f;
    [ReadOnly] public float WaterLoss = 0f;

    [Header("Wood")] public float Wood;
    [ReadOnly] public float WoodGain = 1f;
    [ReadOnly] public float WoodLoss = 0f;

    [Header("Metal")] public float Metal;
    [ReadOnly] public float MetalGain = 1f;
    [ReadOnly] public float MetalLoss = 0f;

    [Header("Energy")] public float Energy;
    [ReadOnly] public float EnergyGain = 1f;
    [ReadOnly] public float EnergyLoss = 0f;

    [Header("Oil")] public float Oil;
    [ReadOnly] public float OilGain = 1f;
    [ReadOnly] public float OilLoss = 0f;

    [Header("Special")] public float Population = 1000;
    [ReadOnly] public float PopulationGain;
    [ReadOnly] public float PopulationLoss;
    public float Generation;
    public float Pollution;
    [ReadOnly] public float PollutionGain;
    [ReadOnly] public float PollutionLoss;

    public int currentPollutionLevel;
    [SerializeField] float[] PollutionLevels;
    [SerializeField] List<float> PollutionForLevel;
    [SerializeField] GameObject gameOverObject;

    [SerializeField] private TMP_Text generationNumText;
    [SerializeField] private Slider pollutionNumText;
    private bool isFirstPlay = true;

    public UnityEvent OnNewGen;

    private int id = -1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        if(isFirstPlay){
            SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource,
            SoundManager.Instance.MusicCollection.clips[3], true, 0);
            isFirstPlay = false;
        }

        StartCoroutine(GameTick());
        efficiency = .75f;
        generationNumText.text = Generation.ToString();
    }

    private void Update()
    {
        generationNumText.text = Generation.ToString();
        pollutionNumText.value = Pollution/1000;
    }

    public void StartGameTick()
    {
        StartCoroutine(GameTick());
    }
[Button]
    public void GameOver()
    {
        SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource,
            SoundManager.Instance.MusicCollection.clips[0], false, 0);
        gameOverObject.SetActive(true);
    }

    public void NewGen()
    {
        int level = (int)PollutionForLevel.Last(x => x <= Pollution);
        level = PollutionForLevel.IndexOf(level);

        currentPollutionLevel = level;
        Generation++;
        generationNumText.text = Generation.ToString();
        PlayerStats.Instance.Start();
        genTime = 0f;
        if (Generation == 5)
            SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource,
                SoundManager.Instance.MusicCollection.clips[2], true, 0);

        Timer.Instance.generationTime = 10;
        Timer.Instance.timeText.text = Timer.Instance.generationTime.ToString();
        OnNewGen.Invoke();
        StartCoroutine(Timer.Instance.TimeCorotuine());
        
    }

    public void ChangeEfficiency(float value)
    {
        if (id != -1)
        {
            efficiency = value;
        }
    }

    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameTickTime);

            Water += WaterGain * PollutionLevels[currentPollutionLevel] * efficiency;
            Wood += WoodGain * PollutionLevels[currentPollutionLevel] * efficiency;
            Metal += MetalGain * PollutionLevels[currentPollutionLevel] * efficiency;
            Energy += EnergyGain * PollutionLevels[currentPollutionLevel] * efficiency;
            Oil += OilGain * PollutionLevels[currentPollutionLevel] * efficiency;

            totalWater += WaterGain * PollutionLevels[currentPollutionLevel];
            totalWood += WoodGain * PollutionLevels[currentPollutionLevel];
            totalMetal += MetalGain * PollutionLevels[currentPollutionLevel];
            totalEnergy += EnergyGain * PollutionLevels[currentPollutionLevel];
            totalOil += OilGain * PollutionLevels[currentPollutionLevel];

            statsText[0].text = Mathf.Floor(Water).ToString();
            statsText[1].text = Mathf.Floor(Wood).ToString();
            statsText[2].text = Mathf.Floor(Metal).ToString();
            statsText[3].text = Mathf.Floor(Energy).ToString();
            statsText[4].text = Mathf.Floor(Oil).ToString();
            statsText[5].text = Mathf.Floor(Population).ToString();

            Water -= WaterLoss;
            Wood -= WoodLoss;
            Metal -= MetalLoss;
            Energy -= EnergyLoss;
            Oil -= OilLoss;

            if (Water <= 0) Water = 0;
            if (Wood <= 0) Wood = 0;
            if (Metal <= 0) Metal = 0;
            if (Energy <= 0) Energy = 0;
            if (Oil <= 0) Oil = 0;


            Pollution += PollutionGain;
            Population += PopulationGain;

            Pollution -= PollutionLoss;
            Population -= PopulationLoss;

            PollutionGain *= efficiency + .25f;

            if (Population <= 0)
            {
                GameOver();
                yield break;
            }

            if (Pollution > 1000)
            {
                Pollution = 1000;
            }

            if (genTime >= GenerationTimer)
            {
                NewGen();
            }

            genTime += GameTickTime;
            gameTime += GameTickTime;
        }
    }
}