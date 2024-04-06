using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUIBehaviour : MonoBehaviour
{
    [SerializeField] UpgradeObject upgradeObject;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text stat;
    [SerializeField] TMP_Text pollution;
    [SerializeField] TMP_Text secondaryStat;

    private void OnEnable()
    {
        switch(upgradeObject.type)
        {
            case UpgradeType.Hub:
                {
                    break;
                }
            case UpgradeType.Pump:
                {
                    break;
                }            
            case UpgradeType.Lumber:
                {
                    break;
                }            
            case UpgradeType.Mine:
                {
                    if (PlayerUpgrades.instance.Mine >= upgradeObject.maxLevel - 1) return;

                    int mineLevel = PlayerUpgrades.instance.Mine;

                    stat.text = $"Current MP/s: {upgradeObject.statIncrease[mineLevel]} <color=#AAFF00> -> {upgradeObject.statIncrease[mineLevel]} </color>";
                    pollution.text = $"Current P/s: {upgradeObject.pollutionIncrease[mineLevel]} <color=#FF0000> -> {upgradeObject.pollutionIncrease[mineLevel]} </color>";
                    secondaryStat.text = $"Energy usage: {upgradeObject.secondaryStatUsage[mineLevel]} <color=#FF0000> -> {upgradeObject.secondaryStatUsage[mineLevel]} </color>";
                    cost.text = $"COST: {upgradeObject.cost[mineLevel]} Materials";
                    break;
                }            
            case UpgradeType.PowerPlant:
                {
                    break;
                }            
            case UpgradeType.OilRig:
                {
                    break;
                }
        }

    }
}
