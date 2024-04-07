using System;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    [Header("Game Settings")]
    public float GameTickTime = 1f;
    public float GenerationTimer = 120f;
    float genTime = 0f;
    [SerializeField] private TMP_Text[] statsText;
    public float gameTime { get; private set; } = 0f;
    
    public float totalWater { get; private set; } = 0f;
    public float totalWood { get; set; } = 0f;
    public float totalMetal { get; private set; } = 0f;
    public float totalEnergy { get; private set; } = 0f;
    public float totalOil { get; private set; } = 0f;

    public float[] efficiences = new float[5];
    


    [Header("Materials")]

    [Header("Water")]
    public float Water;
    [ReadOnly] public float WaterGain = 1f;
    [ReadOnly] public float WaterLoss = 0f;

    [Header("Wood")]
    public float Wood;
    [ReadOnly] public float WoodGain = 1f;
    [ReadOnly] public float WoodLoss = 0f;

    [Header("Metal")]

    public float Metal;
    [ReadOnly] public float MetalGain = 1f ;
    [ReadOnly] public float MetalLoss = 0f ;

    [Header("Energy")]
    public float Energy;
    [ReadOnly] public float EnergyGain = 1f;
    [ReadOnly] public float EnergyLoss = 0f;

    [Header("Oil")]
    public float Oil;
    [ReadOnly] public float OilGain = 1f;
    [ReadOnly] public float OilLoss = 0f;

    [Header("Special")]
    public float Population = 1000;
    [ReadOnly] public float PopulationGain;
    [ReadOnly] public float PopulationLoss;
    public float Generation;
    public float Pollution;
    [ReadOnly] public float PollutionGain;
    [ReadOnly] public float PollutionLoss;

    public int currentPollutionLevel;
    [SerializeField] float[] PollutionLevels;
    [SerializeField] List<float> PollutionForLevel;

    [SerializeField] private TMP_Text generationNumText;
    

    private int id = -1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameTick());
        efficiences[0] = 1f;
        efficiences[1] = 1f;
        efficiences[2] = 0.75f;
        efficiences[3] = 0.75f;
        efficiences[4] = 0.75f;
        generationNumText.text = Generation.ToString();
    }
    public void StartGameTick()
    {
        StartCoroutine(GameTick());
    }

    public void GameOver()
    {

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
        
        Timer.Instance.generationTime = 10;
        Timer.Instance.timeText.text = Timer.Instance.generationTime.ToString();
        StartCoroutine(Timer.Instance.TimeCorotuine());
    }

    public void ChangeEfficiency(float value)
    {
        if (id != -1)
        {
              efficiences[id] = value;
        }
    }
    
    IEnumerator GameTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(GameTickTime);

            Water += WaterGain * PollutionLevels[currentPollutionLevel] * efficiences[0];
            Wood += WoodGain * PollutionLevels[currentPollutionLevel] * efficiences[1];
            Metal += MetalGain * PollutionLevels[currentPollutionLevel] * efficiences[2];
            Energy += EnergyGain * PollutionLevels[currentPollutionLevel] * efficiences[3];
            Oil += OilGain * PollutionLevels[currentPollutionLevel] * efficiences[4];

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

            Pollution *= (efficiences[0] * efficiences[1] * efficiences[2] * efficiences[3] * efficiences[4]) / 5;

            if (Population <= 0)
            {
                GameOver();
                yield break;

            }

            if(Pollution > 1000)
            {
                Pollution = 1000;
            }

            if(genTime >= GenerationTimer)
            {
                NewGen();
            }

            genTime += GameTickTime;
            gameTime += GameTickTime;
        }
    }
}
