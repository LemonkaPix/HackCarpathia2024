using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUIBehaviour : MonoBehaviour
{
    [SerializeField] UpgradeObject upgradeObject;
    [SerializeField] GameObject upgradeError;
    [SerializeField] TMP_Text lvlText;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text[] texts;

    string[] increaseText = new string[]
    {
        "Current People/s: ",
        "Current Water/s: ",
        "Current Wood/s: ",
        "Current Energy/s: ",
        "Current Metal/s: ",
        "Current Oil/s: ",
        "Polution loss: "
    };

    string[] usageText = new string[]
    {
        "People Usage: ",
        "Water usage: ",
        "Wood usage: ",
        "Energy usage: ",
        "Metal usage: ",
        "Oil usage: ",
        "Polution loss: "
    };

    string[] buyText = new string[]
    {
         " People",
         " Water",
         " Wood",
         " Energy",
         " Metal",
         " Oil"
    };

    #region TextReturns

    int GetIndex(UpgradeType type)
    {
        int index = 0;
        switch (type)
        {
            case UpgradeType.Hub:
                index = 0;
                break;
            case UpgradeType.Pump:
                index = 1;
                break;
            case UpgradeType.Lumber:
                index = 2;
                break;
            case UpgradeType.PowerPlant:
                index = 3;
                break;
            case UpgradeType.Mine:
                index = 4;
                break;
            case UpgradeType.OilRig:
                index = 5;
                break;
            case UpgradeType.Pollution:
                index = 6;
                break;
        }
        return index;
}

    float GetMaterialAmount(UpgradeType type)
    {
        float amount = 0;
        switch (type)
        {
            case UpgradeType.Hub:
                amount = PlayerStats.Instance.Population;
                break;
            case UpgradeType.Pump:
                amount = PlayerStats.Instance.Water;
                break;
            case UpgradeType.Lumber:
                amount = PlayerStats.Instance.Wood;
                break;
            case UpgradeType.PowerPlant:
                amount = PlayerStats.Instance.Energy;
                break;
            case UpgradeType.Mine:
                amount = PlayerStats.Instance.Metal;
                break;
            case UpgradeType.OilRig:
                amount = PlayerStats.Instance.Oil;
                break;
        }
        return amount;
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
    #endregion


    void RemoveValue(UpgradeType type, float value)
    {
        switch (type)
        {
            case UpgradeType.Hub:
                PlayerStats.Instance.Population -= value;
                break;
            case UpgradeType.Pump:
                PlayerStats.Instance.Water -= value;
                break;
            case UpgradeType.Lumber:
                PlayerStats.Instance.Wood -= value;
                break;
            case UpgradeType.PowerPlant:
                PlayerStats.Instance.Energy -= value;
                break;
            case UpgradeType.Mine:
                PlayerStats.Instance.Metal -= value;
                break;
            case UpgradeType.OilRig:
                PlayerStats.Instance.Oil -= value;
                break;
        }
    }

    void UpdateGains(UpgradeType type, bool isGain, float value)
    {
        switch (type)
        {
            case UpgradeType.Hub:
                if(isGain) PlayerStats.Instance.PopulationGain = value;
                else PlayerStats.Instance.PopulationLoss = value;
                break;
            case UpgradeType.Pump:
                if(isGain) PlayerStats.Instance.WaterGain = value;
                else PlayerStats.Instance.WaterLoss = value;
                break;
            case UpgradeType.Lumber:
                if (isGain) PlayerStats.Instance.WoodGain = value;
                else PlayerStats.Instance.WoodLoss = value; 
                break;
            case UpgradeType.PowerPlant:
                if (isGain) PlayerStats.Instance.EnergyGain = value;
                else PlayerStats.Instance.EnergyLoss = value; 
                break;
            case UpgradeType.Mine:
                if (isGain) PlayerStats.Instance.MetalGain = value;
                else PlayerStats.Instance.MetalLoss = value; 
                break;
            case UpgradeType.OilRig:
                if (isGain) PlayerStats.Instance.OilGain = value;
                else PlayerStats.Instance.OilLoss = value; 
                break;
            case UpgradeType.Pollution:
                if(isGain) PlayerStats.Instance.PollutionGain = value;
                else PlayerStats.Instance.PopulationLoss = value;
                break;
        }
    }


    void UpdateBuildingStats(int level)
    {
        for (int i = 0; i < upgradeObject.statIncrease.Count; i++)
        {
            UpdateGains(upgradeObject.statIncrease[i].type, true, upgradeObject.statIncrease[i].stats[level]);
        }

        if(upgradeObject.pollutionLevels.Length > 0) PlayerStats.Instance.PollutionGain += upgradeObject.pollutionLevels[level];

        for (int i = 0; i < upgradeObject.statUsage.Count; i++)
        {
            UpdateGains(upgradeObject.statUsage[i].type,false, upgradeObject.statUsage[i].stats[level]);
        }

    }

    [Button]
    public IEnumerator ShowUpgradeError()
    {
        upgradeError.SetActive(true);
        //LeanTween.scale(upgradeError, Vector3.one, .3f);
        LeanTween.rotate(upgradeError, new Vector3(0f,0f,10f), .4f);
        yield return new WaitForSeconds(.4f);
        //LeanTween.rotateX(upgradeError, -10f, .4f);
    }

    public void UpgradeBuilding()
    {
        PlayerUpgrades plrUpg = PlayerUpgrades.instance;
        float materialAmount = GetMaterialAmount(upgradeObject.cost.type);

        if(upgradeObject.type != UpgradeType.Hub && plrUpg.hubLevel < GetBuildingLevel(upgradeObject.type) + 1)
        {
            print("Hub level too low");
            ShowUpgradeError();
            return;
        }

        switch (upgradeObject.type)
        {
            case UpgradeType.Hub:
                if (plrUpg.hubLevel < upgradeObject.maxLevel && materialAmount > upgradeObject.cost.cost[plrUpg.hubLevel])
                {
                    RemoveValue(upgradeObject.type, upgradeObject.cost.cost[plrUpg.hubLevel]);
                    plrUpg.hubLevel++;

                    UpdateBuildingStats(plrUpg.hubLevel);
                }
                break;
            case UpgradeType.Pump:
                if (plrUpg.Pump < upgradeObject.maxLevel && materialAmount > upgradeObject.cost.cost[plrUpg.Pump])
                {
                    RemoveValue(upgradeObject.type, upgradeObject.cost.cost[plrUpg.Pump]);
                    plrUpg.Pump++;

                    UpdateBuildingStats(plrUpg.Pump);
                }
                break;
            case UpgradeType.Lumber:
                if (plrUpg.lumberMill < upgradeObject.maxLevel && materialAmount > upgradeObject.cost.cost[plrUpg.lumberMill])
                {
                    RemoveValue(upgradeObject.type, upgradeObject.cost.cost[plrUpg.lumberMill]);
                    plrUpg.lumberMill++;

                    UpdateBuildingStats(plrUpg.lumberMill);
                }
                break;
            case UpgradeType.PowerPlant:
                if (plrUpg.powerPlant < upgradeObject.maxLevel && materialAmount > upgradeObject.cost.cost[plrUpg.powerPlant])
                {
                    RemoveValue(upgradeObject.type, upgradeObject.cost.cost[plrUpg.powerPlant]);
                    plrUpg.powerPlant++;

                    UpdateBuildingStats(plrUpg.powerPlant);
                }
                break;
            case UpgradeType.Mine:
                if (plrUpg.Mine < upgradeObject.maxLevel && materialAmount > upgradeObject.cost.cost[plrUpg.Mine])
                {
                    RemoveValue(upgradeObject.type, upgradeObject.cost.cost[plrUpg.Mine]);
                    plrUpg.Mine++;

                    UpdateBuildingStats(plrUpg.Mine);
                }
                break;
            case UpgradeType.OilRig:
                if (plrUpg.OilRig < upgradeObject.maxLevel && materialAmount > upgradeObject.cost.cost[plrUpg.OilRig])
                {
                    RemoveValue(upgradeObject.type, upgradeObject.cost.cost[plrUpg.OilRig]);
                    plrUpg.OilRig++;

                    UpdateBuildingStats(plrUpg.OilRig);
                }
                break;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        int lastIndex = 0;
        int level = GetBuildingLevel(upgradeObject.type);
        if (level >= upgradeObject.maxLevel - 1) return;

        for (int i = 0; i < upgradeObject.statIncrease.Count; i++)
        {
            UpgradeStatIncrease stat = upgradeObject.statIncrease[i];

            string msg = "";

            msg += increaseText[GetIndex(stat.type)];
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
            msg += usageText[GetIndex(stat.type)];
            if(stat.type == UpgradeType.Pollution)
            {
                msg += $"{stat.stats[level]}<color=#AAFF00> -> {stat.stats[level + 1]} </color>";
            }
            else msg += $"{stat.stats[level]}<color=#FF0000> -> {stat.stats[level + 1]} </color>";
            texts[i + lastIndex].text = msg;
        }

        cost.text = $"{upgradeObject.cost.cost[level]} {buyText[GetIndex(upgradeObject.cost.type)]}";
        lvlText.text = $"Lv. {level + 1}";
    }

    public void CloseWindow()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.2f);

        Invoke(nameof(disableGameObject), 0.3f);
    }

    void disableGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (gameObject.transform.localScale == Vector3.one) gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.2f);
        UpdateUI();
    }
    private void Start()
    {
        //gameObject.SetActive(false);
    }

}

