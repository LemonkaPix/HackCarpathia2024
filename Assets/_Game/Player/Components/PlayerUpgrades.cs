    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades instance;

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

}
