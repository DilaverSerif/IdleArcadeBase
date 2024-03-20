using UnityEngine;

[CreateAssetMenu(fileName = "StatController", menuName = "ScriptableObjects/StatController", order = 1)]
public class StatController : SingletonScriptableObject<StatController>
{
    public BaseStatData[] statData;
    
    public float GetStatMinValue(StatType statType)
    {
        foreach (var stat in statData)
        {
            if (stat.statType == statType)
            {
                return stat.minValue;
            }
        }
        return 0;
    }

    public Sprite GetStatIcon(StatType statType)
    {
        foreach (var stat in statData)
        {
            if (stat.statType == statType)
            {
                return stat.icon;
            }
        }
        return null;
    }
    
    public float GetStatMaxValue(StatType statType)
    {
        foreach (var stat in statData)
        {
            if (stat.statType == statType)
            {
                return stat.maxValue;
            }
        }
        return 0;
    }
    
    public float GetStatBaseValue(StatType statType)
    {
        foreach (var stat in statData)
        {
            if (stat.statType == statType)
            {
                return stat.baseValue;
            }
        }
        return 0;
    }
    
    public float GetStatValueMultiplier(StatType statType)
    {
        foreach (var stat in statData)
        {
            if (stat.statType == statType)
            {
                return stat.valueMultiplier;
            }
        }
        return 0;
    }
    
    public float GetStatValueByLevel(StatType statType, int level)
    {
        var currentValue = GetStatBaseValue(statType) + GetStatValueMultiplier(statType) * level;
        return Mathf.Clamp(currentValue, GetStatMinValue(statType), GetStatMaxValue(statType));
    }
}