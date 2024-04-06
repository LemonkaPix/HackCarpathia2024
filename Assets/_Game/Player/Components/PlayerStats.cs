using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Game Settings")]
    public float GameTickTime = 1f;

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
    public float People;
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

            Pollution += PollutionGain;
            if(Pollution > 1000)
            {
                Pollution = 1000;
            }

            int level = (int)PollutionForLevel.Last(x => x <= Pollution);
            level = PollutionForLevel.IndexOf(level);

            currentPollutionLevel = level;

        }
    }
}
