using System;
using UnityEngine;

[Serializable] 
public struct BaseStatData
{
    public StatType statType;
    public float baseValue;
    public float valueMultiplier;
    
    public float maxValue;
    public float minValue;
    public Sprite icon;
}