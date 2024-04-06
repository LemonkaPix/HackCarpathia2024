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
    Pollution
}

[System.Serializable]
public class UpgradeStatIncrease
{
    public UpgradeType type;
    public float[] stats;
}


[System.Serializable]
public class UpgCost
{
    public UpgradeType type;
    public float[] cost;
}

[CreateAssetMenu]
public class UpgradeObject : ScriptableObject
{
    [Header("Main")]
    public string name;
    public UpgradeType type;
    public UpgCost cost;
    public int maxLevel = 10;

    [Header("Stats")]
    public List<UpgradeStatIncrease> statIncrease;
    public float[] pollutionLevels;
    public List<UpgradeStatIncrease> statUsage;
}
