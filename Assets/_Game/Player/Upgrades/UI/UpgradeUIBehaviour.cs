using TMPro;
using UnityEngine;

public class UpgradeUIBehaviour : MonoBehaviour
{
    [SerializeField] UpgradeObject upgradeObject;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text[] texts;


    string AddIncreaseText(UpgradeType stat)
    {
        string msg = "";
        switch (stat)
        {
            case UpgradeType.Hub:
                msg += "Current People/s: ";
                break;
            case UpgradeType.Pump:
                msg += "Current Water/s: ";
                break;
            case UpgradeType.Lumber:
                msg += "Current Wood/s: ";
                break;
            case UpgradeType.PowerPlant:
                msg += "Current Energy/s: ";
                break;
            case UpgradeType.Mine:
                msg += "Current Material/s: ";
                break;
            case UpgradeType.OilRig:
                msg += "Current Oil/s: ";
                break;
        }
        return msg;
    }


    string AddUsageText(UpgradeType stat)
    {
        string msg = "";
        switch (stat)
        {
            case UpgradeType.Hub:
                msg += "People Usage: ";
                break;
            case UpgradeType.Pump:
                msg += "Water usage: ";
                break;
            case UpgradeType.Lumber:
                msg += "Wood usage: ";
                break;
            case UpgradeType.PowerPlant:
                msg += "Energy usage: ";
                break;
            case UpgradeType.Mine:
                msg += "Material usage: ";
                break;
            case UpgradeType.OilRig:
                msg += "Oil usage: ";
                break;
        }
        return msg;
    }

    int GetBuildingLevel(UpgradeType type)
    {
        int level = 0;
        switch (type)
        {
            case UpgradeType.Hub:
                level = PlayerUpgrades.instance.hubLevel;
                break;
            case UpgradeType.Pump:
                level = PlayerUpgrades.instance.Pump;
                break;
            case UpgradeType.Lumber:
                level = PlayerUpgrades.instance.lumberMill;
                break;
            case UpgradeType.PowerPlant:
                level = PlayerUpgrades.instance.powerPlant;
                break;
            case UpgradeType.Mine:
                level = PlayerUpgrades.instance.Mine;
                break;
            case UpgradeType.OilRig:
                level = PlayerUpgrades.instance.OilRig;
                break;
        }
        return level;
    }

    private void OnEnable()
    {
        int lastIndex = 0;
        int level = GetBuildingLevel(upgradeObject.type);
        if (level >= upgradeObject.maxLevel - 1) return;

        for (int i = 0; i < upgradeObject.statIncrease.Count; i++)
        {
            UpgradeStatIncrease stat = upgradeObject.statIncrease[i];

            string msg = "";

            msg += AddIncreaseText(stat.type);
            msg += $"{stat.stats[level]}<color=#AAFF00> -> {stat.stats[level + 1]} </color>";
            texts[i].text = msg;
            lastIndex = i + 1;
        }

        if (upgradeObject.pollutionLevels.Length > 0)
        {
            texts[lastIndex].text = $"Current P/s: {upgradeObject.pollutionLevels[level]} <color=#FF0000> -> {upgradeObject.pollutionLevels[level + 1]}";
            lastIndex++;
        }

        for (int i = 0; i < upgradeObject.statUsage.Count; i++)
        {
            UpgradeStatIncrease stat = upgradeObject.statUsage[i];

            string msg = "";
            msg += AddUsageText(stat.type);
            msg += $"{stat.stats[level]}<color=#FF0000> -> {stat.stats[level + 1]} </color>";
            texts[i + lastIndex].text = msg;
        }


        //switch (upgradeObject.type)
        //{
        //    case UpgradeType.Hub:
        //        {
        //            if (PlayerUpgrades.instance.hubLevel >= upgradeObject.maxLevel - 1) return;

        //            int level = PlayerUpgrades.instance.hubLevel;

                    

        //            texts[0].text = $"Current People/s: {upgradeObject.upgradeStats[0].stats[level]} <color=#AAFF00> -> {upgradeObject.statIncrease[level]} </color>";
        //            texts[1].text = $"Wood usage: {upgradeObject.upgradeStats[1].stats[level]} <color=#FF0000> -> {upgradeObject.pollutionIncrease[level]} </color>";
        //            texts[2].text = $"Energy usage: {upgradeObject.secondaryStatUsage[level]} <color=#FF0000> -> {upgradeObject.secondaryStatUsage[level]} </color>";
        //            cost.text = $"COST: {upgradeObject.cost[level]} Materials";
        //            break;
        //        }
        //    case UpgradeType.Pump:
        //        {
        //            break;
        //        }            
        //    case UpgradeType.Lumber:
        //        {
        //            break;
        //        }            
        //    case UpgradeType.Mine:
        //        {
        //            if (PlayerUpgrades.instance.Mine >= upgradeObject.maxLevel - 1) return;

        //            int level = PlayerUpgrades.instance.Mine;

        //            texts[0].text = $"Current MP/s: {upgradeObject.statIncrease[level]} <color=#AAFF00> -> {upgradeObject.statIncrease[level]} </color>";
        //            texts[1].text = $"Current P/s: {upgradeObject.pollutionIncrease[level]} <color=#FF0000> -> {upgradeObject.pollutionIncrease[level]} </color>";
        //            texts[2].text = $"Energy usage: {upgradeObject.secondaryStatUsage[level]} <color=#FF0000> -> {upgradeObject.secondaryStatUsage[level]} </color>";
        //            cost.text = $"COST: {upgradeObject.cost[level]} Materials";
        //            break;
        //        }            
        //    case UpgradeType.PowerPlant:
        //        {
        //            break;
        //        }            
        //    case UpgradeType.OilRig:
        //        {
        //            break;
        //        }
        //}


    }
}
