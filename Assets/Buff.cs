using System;
using Sirenix.OdinInspector;
[Serializable]
public struct Buff
{
    public StatType statType;
    public bool isPermanent;

    [ShowIf("isPermanent", false)]
    public float timeLeft;
    [ShowIf("isPermanent", false)]
    public float duration;
    
    public float value;
    public MathOperation operation;
}