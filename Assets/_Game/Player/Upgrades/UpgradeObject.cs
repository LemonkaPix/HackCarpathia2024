using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Hub,
    Pump,
    Lumber,
    Mine,
    PowerPlant,
    OilRig,
}


[CreateAssetMenu]
public class UpgradeObject : ScriptableObject
{
    [Header("Main")]
    public string name;
    public UpgradeType type;
    public int maxLevel = 10;

    [Header("Stats")]
    public float[] cost;
    public float[] statIncrease;
    public float[] pollutionIncrease;
    public float[] secondaryStatUsage;
}
