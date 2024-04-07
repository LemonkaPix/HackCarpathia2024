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
        if(hubLevel == 5 && Pump == 5 && lumberMill == 5 && Mine == 5 && powerPlant == 5 && waterPowerStation == 5 && OilRig == 5)
        {
            print("Game over!!!");
        }
    }

}
