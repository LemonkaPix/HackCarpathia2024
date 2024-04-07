using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades instance;
    [SerializeField] GameObject winnerObject;

    public int hubLevel;
    [Header("Water")]
    public int Pump;
    [Header("Wood")]
    public int lumberMill;
    [Header("Mineral")]
    public int Mine;
    [Header("Energy")]
    public int powerPlant;
    public int waterPowerStation;
    [Header("Oil")]
    public int OilRig;

    private void Awake()
    {
        instance = this;
    }

    public void CheckBuildings()
    {
        if(hubLevel == 10 && Pump == 10 && lumberMill == 10 && Mine == 10 && powerPlant == 10 && waterPowerStation == 10 && OilRig == 10)
        {
            print("Game over!!!");
        }
    }

}
