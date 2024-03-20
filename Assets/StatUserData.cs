using System;
using Sirenix.OdinInspector;
[Serializable]
public class StatUserData
{
    public StatType statType;
    public int level;
    
    [ReadOnly,ShowInInspector]
    public float CurrentValue => StatController.Instance.GetStatValueByLevel(statType, level);
    
    public void AddLevel()
    {
        level++;
    }
}