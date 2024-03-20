using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PlayerInGameData
{
    public enum RotationType
    {
        None,
        LookAt,
        Lerp,
        Slerp
    }
    
    [BoxGroup("Movement")]
    public AnimationCurve moveSpeedCurve;
    [BoxGroup("Movement")]
    public float acceleration;
    
    [BoxGroup("Rotation")] 
    public AnimationCurve rotationTurnSpeedCurve;
    [BoxGroup("Rotation")] 
    public RotationType rotationType;

    [NonSerialized,ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float CurrentWalkTime;
    [NonSerialized,ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float CurrentMaxWalkTime;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float maxWalkTime;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float maxSpeed;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public Vector3 targetPosition;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public Quaternion targetRotation;
    
    private PlayerBrain playerBrain;
    private PlayerDefaultData DefaultData => playerBrain.defaultData;
    private StatUser StatUser => playerBrain.statUser;
    public PlayerInGameData(PlayerBrain playerBrain)
    {
        this.playerBrain = playerBrain;
        
        moveSpeedCurve = DefaultData.movementCurve;
        acceleration = DefaultData.acceleration + StatUser.GetStat(StatType.MoveAcceleration);
        rotationTurnSpeedCurve = DefaultData.rotationTurnSpeedCurve;
        rotationType = DefaultData.rotationType;
        
        maxWalkTime = moveSpeedCurve.keys[moveSpeedCurve.length - 1].time;
        maxSpeed = moveSpeedCurve.keys[moveSpeedCurve.length - 1].value + StatUser.GetStat(StatType.MoveSpeed);
        
        StatUser.OnAddedStat += RefreshStats;
    }
    
    public void Dispose()
    {
        StatUser.OnAddedStat -= RefreshStats;
    }
    
    void RefreshStats(AddedStat obj)
    {
        switch (obj.statType)
        {
            case StatType.MoveSpeed:
                var moveSpeedCurveKeys = moveSpeedCurve.keys;
                moveSpeedCurveKeys[^1].value = DefaultData.movementCurve.keys[^1].value + StatUser.GetStat(StatType.MoveSpeed);
                moveSpeedCurve = new AnimationCurve(moveSpeedCurveKeys);
                
                var rotationTurnSpeedCurveKeys = rotationTurnSpeedCurve.keys;
                rotationTurnSpeedCurveKeys[^1].value = DefaultData.rotationTurnSpeedCurve.keys[^1].value + StatUser.GetStat(StatType.MoveSpeed);
                rotationTurnSpeedCurve = new AnimationCurve(rotationTurnSpeedCurveKeys);
                break;
            
            case StatType.MoveAcceleration:
                acceleration = DefaultData.acceleration + StatUser.GetStat(StatType.MoveAcceleration);
                break;
        }
    }
}