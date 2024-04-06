using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Game Settings")]
    public float GameTickTime = 1f;
    public float GenerationTimer = 120f;
    float genTime = 0f;
    public float gameTime { get; private set; } = 0f;
    public float totalWater { get; private set; } = 0f;
    public float totalWood { get; private set; } = 0f;
    public float totalMetal { get; private set; } = 0f;
    public float totalEnergy { get; private set; } = 0f;
    public float totalOil { get; private set; } = 0f;

    [Header("Materials")]

    [Header("Water")]
    public float Water;
    [ReadOnly] public float WaterGain = 1f;

    [Header("Wood")]
    public float Wood;
    [ReadOnly] public float WoodGain = 1f;

    [Header("Metal")]

    public float Metal;
    [ReadOnly] public float MetalGain = 1f ;

    [Header("Energy")]
    public float Energy;
    [ReadOnly] public float EnergyGain = 1f;

    [Header("Oil")]
    public float Oil;
    [ReadOnly] public float OilGain = 1f;

    [Header("Special")]
    public float Population = 1000;
    [ReadOnly] public float PopulationGain;
    public float Generation;
    public float Pollution;
    [ReadOnly] public float PollutionGain;

    public int currentPollutionLevel;
    [SerializeField] float[] PollutionLevels;
    [SerializeField] List<float> PollutionForLevel;


    private void Start()
    {
        StartCoroutine(GameTick());

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
        genTime = 0f;
    }

    IEnumerator GameTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(GameTickTime);

            Water += WaterGain * PollutionLevels[currentPollutionLevel];
            Wood += WoodGain * PollutionLevels[currentPollutionLevel];
            Metal += MetalGain * PollutionLevels[currentPollutionLevel];
            Energy += EnergyGain * PollutionLevels[currentPollutionLevel];
            Oil += OilGain * PollutionLevels[currentPollutionLevel];

            totalWater += WaterGain * PollutionLevels[currentPollutionLevel];
            totalWood += WoodGain * PollutionLevels[currentPollutionLevel];
            totalMetal += MetalGain * PollutionLevels[currentPollutionLevel];
            totalEnergy += EnergyGain * PollutionLevels[currentPollutionLevel];
            totalOil += OilGain * PollutionLevels[currentPollutionLevel];


            Pollution += PollutionGain;
            Population += PopulationGain;


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
            Debug.Log("Loop");
        }
    }
}
